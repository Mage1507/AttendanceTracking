using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using AwsSES.Constants;
using AwsSES.Interface;
using MimeKit;

namespace AwsSES.Service;

public class SendEmail:ISendEmail
{
    
    public async Task<bool> send(string awsAccessKey,string awsSecretKey,string awsSessionToken,int managerId)
    {

        using (var client =
               new AmazonSimpleEmailServiceClient(awsAccessKey, awsSecretKey, awsSessionToken, RegionEndpoint.USEast1))
        {

            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = EmailConstants.BODY;
            bodyBuilder.TextBody = EmailConstants.BODY;

            using (FileStream fileStream =
                   new FileStream($"../AttendanceTracking/AttendanceReport/{managerId + "_AttendanceReport"}.pdf",
                       FileMode.Open, FileAccess.Read))
            {
                bodyBuilder.Attachments.Add($"{managerId + "_AttendanceReport"}.pdf", fileStream);
            }

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(EmailConstants.FROMNAME, EmailConstants.FROM));
            mimeMessage.To.Add(new MailboxAddress(EmailConstants.TONAME, EmailConstants.TO));

            mimeMessage.Subject = EmailConstants.SUBJECT;
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            var messageStream = new MemoryStream();
            await mimeMessage.WriteToAsync(messageStream);
            var sendRequest = new SendRawEmailRequest { RawMessage = new RawMessage(messageStream) };
            var response = await client.SendRawEmailAsync(sendRequest);
            Console.WriteLine("Response:"+response);
            if (response.MessageId != null)
            {
                return true;
            }

             return false;

        }


    }
}