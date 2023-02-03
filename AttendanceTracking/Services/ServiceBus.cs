using System.Text;
using System.Text.Json;
using AttendanceTracking.Data.ResponseModels;
using AttendanceTracking.Models;
using Microsoft.Azure.ServiceBus;

namespace AttendanceTracking.Services;

public class ServiceBus
{
    private readonly IConfiguration _configuration;
    public ServiceBus(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendMessageAsync(Attendance attendance,string employeeEmail)
    {
        attendance.employee.employeeEmail = employeeEmail;
        IQueueClient client = new QueueClient(_configuration["AzureServiceBusConnectionString"], _configuration["QueueName"]);
        //Serialize attendance details object
        var messageBody = JsonSerializer.Serialize(attendance);
        //Set content type and Guid
        var message = new Message(Encoding.UTF8.GetBytes(messageBody))
        {
            MessageId = Guid.NewGuid().ToString(),
            ContentType = "application/json"
        };
        await client.SendAsync(message);
    }
}