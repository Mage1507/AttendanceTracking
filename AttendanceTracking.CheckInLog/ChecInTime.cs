using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure.Communication.Email;
using Azure.Communication.Email.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AttendanceTracking.CheckInLog;

public static class ChecInTime
{
    [FunctionName("ChecInTime")]
    public static async Task RunAsync([ServiceBusTrigger("checkinqueue", Connection = "AzureServiceBusConnectionString")] string myQueueItem, ILogger log)
    {
        log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        dynamic deserialize = JsonConvert.DeserializeObject(myQueueItem);
        string connectionString = Environment.GetEnvironmentVariable("CommunicationServiceConnectionString");
        EmailClient emailClient = new EmailClient(connectionString);
        EmailContent emailContent = new EmailContent($"{Constants.EmailSubject}");
        emailContent.PlainText =
            $"You have been checked out of office at {deserialize.checkOutTime} and Your total time spent in office is {deserialize.totalPresentTime} from your check in time {deserialize.checkInTime}";
        List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress($"{deserialize.employee.employeeEmail}") { DisplayName = "" } };
        EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
        EmailMessage emailMessage = new EmailMessage($"{Constants.FromEmail}", emailContent, emailRecipients);
        SendEmailResult emailResult = emailClient.Send(emailMessage, CancellationToken.None);
    }
}