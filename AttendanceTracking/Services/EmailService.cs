using System;
using Azure.Communication.Email;
using Azure.Communication.Email.Models;

namespace AttendanceTracking.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        private readonly ManagerService _managerService;

        public EmailService(IConfiguration configuration, ManagerService managerService)
        {
            _configuration = configuration;
            _managerService = managerService;
        }

        //Send attendance report as email attachment to respective manager
        public bool SendEmail(int managerId)
        {
            var managerEmail = _managerService.GetManagerEmail(managerId);
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
