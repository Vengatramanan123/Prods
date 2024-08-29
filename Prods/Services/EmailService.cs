using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Prods.Utilites;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Prods.Services
{
    public class EmailService
    {
        private readonly string _apiKeyPublic;
        private readonly string _apiKeyPrivate;

        public EmailService(IOptions<MailjetSettings> mailjetSettings)
        {
            // Retrieve API keys from environment variables
            _apiKeyPublic = mailjetSettings.Value.PublicApiKey;
            _apiKeyPrivate = mailjetSettings.Value.PrivateApiKey;

            if (string.IsNullOrEmpty(_apiKeyPublic) || string.IsNullOrEmpty(_apiKeyPrivate))
            {
                throw new InvalidOperationException("API keys are not set correctly.");
            }
        }

        public async Task<bool> SendEmailAsync(string toEmail, string toName, string subject, string textContent, string htmlContent)
        {
            // Initialize MailjetClient with API keys
            MailjetClient client = new MailjetClient(_apiKeyPublic, _apiKeyPrivate);

            // Create the message object
            var message = new JObject
    {
        {"FromEmail", "YOUR_VERIFIED_MAIL_IN_MAILJET"},
        {"FromName", "Prods"},
        {"Recipients", new JArray
            {
                new JObject
                {
                    {"Email", toEmail},
                    {"Name", toName}
                }
            }
        },
        {"Subject", subject},
        {"Text-part", textContent},
        {"Html-part", htmlContent}
    };


            // Create the Mailjet request
            var request = new MailjetRequest
            {
                Resource = Send.Resource
            }
            .Property(Send.Messages, new JArray { message });

            try
            {
                // Send the email
                MailjetResponse response = await client.PostAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Email sent successfully: {response.GetData()}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Failed to send email: {response.StatusCode} - {response.GetErrorMessage()}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while sending email: {ex.Message}");
                return false;
            }
        }
    }
}
