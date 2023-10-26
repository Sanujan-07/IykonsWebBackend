using Amazon.SimpleEmail.Model;
using Amazon.SimpleEmail;
using Iycons_web2._0.DTO;
using Amazon;
using System.Threading.Tasks;
using Iycons_web2._0.Model;

namespace Iycons_web2._0.Service
{
    public class EmailService
    {
        private readonly string sesAccessKey = "YOUR_SES_ACCESS_KEY";
        private readonly string sesSecretKey = "YOUR_SES_SECRET_KEY";
        private readonly string senderEmail = "your-sender@example.com"; // Replace with your sender email address
        private readonly string recipientEmail = "recipient@example.com"; // Replace with the recipient's email address
        /*
        private readonly List<string> recipientEmails = new List<string>
         {
            "recipient1@example.com",
            "recipient2@example.com",
            // Add more recipient email addresses as needed
         };*/
        public void SendFeedbackEmail(FeedbackDto feedback)
        {
            // Set up SES client
            var sesClient = new AmazonSimpleEmailServiceClient(
                sesAccessKey,
                sesSecretKey,
                RegionEndpoint.USWest2); // Change the region to your desired AWS region

            // Compose and send an email
            var sendRequest = new SendEmailRequest
            {
                Source = senderEmail,
                Destination = new Destination
                {
                    ToAddresses = new List<string> { recipientEmail }
                },
                Message = new Message
                {
                    Subject = new Content("Feedback Received"),
                    Body = new Body
                    {
                        Html = new Content($"<p>Name: {feedback.Name}</p><p>Email: {feedback.Email}</p><p>Subject: {feedback.Subject}</p><p>Message: {feedback.Message}</p>")
                    }
                }
            };

            try
            {
                var sendResponse = sesClient.SendEmailAsync(sendRequest);
                Console.WriteLine("Email sent successfully!");
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
            }
        }
        public async Task SendContactUsEmail(ContactUs contact)
        {
            var s3Client = new AmazonSimpleEmailServiceClient(sesAccessKey, sesSecretKey, RegionEndpoint.USWest2); // Change the region to your desired AWS region

            try
            {
                var subject = "Contact Us Inquiry";
                var body = $"<p>Name: {contact.Name}</p><p>Email: {contact.Email}</p><p>Subject: {contact.Subject}</p><p>Message: {contact.Message}</p>";

                var sendRequest = new SendEmailRequest
                {
                    Source = senderEmail,
                    Destination = new Destination
                    {
                        ToAddresses = new List<string> { recipientEmail }
                    },
                    Message = new Message
                    {
                        Subject = new Content(subject),
                        Body = new Body
                        {
                            Html = new Content(body)
                        }
                    }
                };

                var response = await s3Client.SendEmailAsync(sendRequest);
                Console.WriteLine("Email sent successfully!");
                Console.WriteLine($"Message ID: {response.MessageId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email sending failed: " + ex.Message);
            }
        }
    }

}
