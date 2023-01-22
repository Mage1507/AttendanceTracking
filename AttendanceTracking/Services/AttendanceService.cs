using System;
using System.IO;
using AttendanceTracking.Data;
using AttendanceTracking.Data.ResponseModels;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Models;
using AutoMapper;
using Azure.Communication.Email;
using Azure.Communication.Email.Models;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace AttendanceTracking.Services
{
    public class AttendanceService
    {
        private readonly DbInitializer _dbContext;

        private readonly EmployeeService _employeeService;

        private readonly ManagerService _managerService;

        private readonly ILogger<AttendanceService> _logger;

        private readonly IMapper _mapper;

        private readonly IConfiguration _configuration;

        public AttendanceService(
            DbInitializer dbContext,
            EmployeeService employeeService,
            ManagerService manager,
            ILogger<AttendanceService> logger,
            IMapper mapper,
            IConfiguration configuration
        )
        {
            _dbContext = dbContext;
            _employeeService = employeeService;
            _managerService = manager;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
        }

        public bool LogCheckIn(CheckInTimeVM checkInTimeVM)
        {
            int count = 0;
            if (checkInTimeVM == null)
            {
                return false;
            }
            try
            {
                var attendanceList = GetAttendanceListByEmployeeId(checkInTimeVM.employeeId);
                foreach (var att in attendanceList)
                {
                    string date = att.date.ToShortDateString();
                    if (date == DateTime.Now.ToShortDateString() && att.checkOutTime == null)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    Attendance attendance = new Attendance()
                    {
                        employeeId = checkInTimeVM.employeeId,
                        date = DateTime.Now.Date,
                        checkInTime = DateTime.UtcNow
                    };
                    _dbContext.attendances.Add(attendance);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in LogCheckIn Method :" + ex.Message);
                return false;
            }
        }

        public bool LogCheckOut(CheckOutTimeVM checkOutTimeVM)
        {
            int count = 0;
            if (checkOutTimeVM == null)
            {
                return false;
            }
            try
            {
                var attendanceList = GetAttendanceList(checkOutTimeVM.employeeId);
                foreach (var att in attendanceList)
                {
                    string date = att.date.ToShortDateString();
                    if (date == DateTime.Now.ToShortDateString() && att.checkOutTime == null)
                    {
                        att.checkOutTime = DateTime.UtcNow;
                        _dbContext.attendances.Update(att);
                        _dbContext.SaveChanges();
                        count++;
                        break;
                    }
                }
                if (count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception:" + ex.Message);
                return false;
            }
        }

        public List<AttendanceResponse> GetAttendanceOfEmployee(
            int managerId,
            DateTime date,
            DateTime fromTime,
            DateTime toTime
        )
        {
            var employeeList = GetAttendanceListByManagerId(managerId);
            var attendanceList = new List<AttendanceResponse>();
            foreach (var emp in employeeList)
            {
                TimeSpan? ts = new TimeSpan(0, 0, 0);
                var attendance = GetAttendanceListByEmployeeId(emp.employeeId);
                foreach (var att in attendance)
                {
                    _logger.LogInformation(
                        "CheckOut Time:"
                            + att.checkOutTime?.ToLocalTime()
                            + "CheckIn Time:"
                            + att.checkInTime.ToLocalTime()
                    );

                    if (
                        att.date == date
                        && att.checkInTime.ToLocalTime() >= fromTime
                        && att.checkOutTime?.ToLocalTime() <= toTime
                    )
                    {
                        att.totalPresentTime =
                            att.checkOutTime?.ToLocalTime() - att.checkInTime.ToLocalTime();
                        _logger.LogInformation("Total Present Time:" + att.totalPresentTime);
                        ts = ts + att.totalPresentTime;
                        _logger.LogInformation("Total Present Time:" + ts);
                        att.totalHoursInOffice = ts;
                        att.checkOutTime = att.checkOutTime?.ToLocalTime();
                        att.checkInTime = att.checkInTime.ToLocalTime();
                        att.employeeEmail = _employeeService.GetEmployeeEmailById(att.employeeId);
                        attendanceList.Add(att);
                    }
                }
            }
            bool sendEmail = SendReportEmail(attendanceList, managerId);
            if (sendEmail)
            {
                return attendanceList;
            }
            else
            {
                return null;
            }
        }

        public List<Employee> GetAttendanceListByManagerId(int managerId)
        {
            var employeeList = _employeeService.GetEmployeeListByManagerId(managerId);
            return employeeList;
        }

        public List<AttendanceResponse> GetAttendanceListByEmployeeId(int employeeId)
        {
            var attendanceList = _mapper.Map<List<AttendanceResponse>>(
                _dbContext.attendances.Where(x => x.employeeId == employeeId).ToList()
            );
            return attendanceList;
        }

        public List<Attendance> GetAttendanceList(int employeeId)
        {
            var attendanceList = _dbContext.attendances
                .Where(x => x.employeeId == employeeId)
                .ToList();
            return attendanceList;
        }

        public bool SendReportEmail(List<AttendanceResponse> attendanceList, int managerId)
        {
            var managerEmail = _managerService.GetManagerEmail(managerId);
            PdfWriter writer = new PdfWriter(
                $"../AttendanceTracking/AttendanceReport/{managerId + "_AttendanceReport"}.pdf"
            );
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Table table = new Table(7);
            table.SetWidth(100);
            table.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            Cell cell11 = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Employee ID"));
            Cell cell12 = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Employee Email"));
            Cell cell13 = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Date"));
            Cell cell14 = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Check In Time"));
            Cell cell15 = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Check Out Time"));
            Cell cell16 = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Total Present Time"));
            Cell cell17 = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Total Hours In Office"));

            table.AddCell(cell11);
            table.AddCell(cell12);
            table.AddCell(cell13);
            table.AddCell(cell14);
            table.AddCell(cell15);
            table.AddCell(cell16);
            table.AddCell(cell17);

            foreach (var att in attendanceList)
            {
                Cell cell21 = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(att.employeeId.ToString()));
                table.AddCell(cell21);
                Cell cell22 = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(att.employeeEmail.ToString()));
                table.AddCell(cell22);
                Cell cell23 = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(att.date.ToString()));
                table.AddCell(cell23);
                Cell cell24 = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(att.checkInTime.ToString()));
                table.AddCell(cell24);
                Cell cell25 = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(att.checkOutTime.ToString()));
                table.AddCell(cell25);
                Cell cell26 = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(att.totalPresentTime.ToString()));
                table.AddCell(cell26);
                Cell cell27 = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(att.totalHoursInOffice.ToString()));
                table.AddCell(cell27);
            }
            document.Add(table);
            document.Close();

            string connectionString = _configuration["communicationService"];
            EmailClient emailClient = new EmailClient(connectionString);
            EmailContent emailContent = new EmailContent("Attendance Report");
            emailContent.PlainText = "Attendance Report";
            List<EmailAddress> emailAddresses = new List<EmailAddress>
            {
                new EmailAddress(managerEmail) { DisplayName = "Friendly Display Name" }
            };
            EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
            EmailMessage emailMessage = new EmailMessage(
                _configuration["communicationService:FromEmail"],
                emailContent,
                emailRecipients
            );
            byte[] bytes = File.ReadAllBytes(
                $"../AttendanceTracking/AttendanceReport/{managerId + "_AttendanceReport"}.pdf"
            );
            string attachmentFileInBytes = Convert.ToBase64String(bytes);
            var emailAttachment = new EmailAttachment(
                $"{managerId + "_AttendanceReport"}.pdf",
                EmailAttachmentType.Pdf,
                attachmentFileInBytes
            );

            emailMessage.Attachments.Add(emailAttachment);
            SendEmailResult emailResult = emailClient.Send(emailMessage, CancellationToken.None);
            if (emailResult.MessageId != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
