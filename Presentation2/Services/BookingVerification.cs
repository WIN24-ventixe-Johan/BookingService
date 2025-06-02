using SendGrid;
using SendGrid.Helpers.Mail;


namespace Presentation2.Services
{
    public class BookingVerification
    {
        private readonly IConfiguration _config;

        public BookingVerification(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            Console.WriteLine("🔍 SENDGRID_API_KEY loaded: " + (!string.IsNullOrEmpty(apiKey)));

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("SENDGRID_API_KEY is missing.");
            }

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("johanstroberg91@gmail.com", "Ventixe");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);

            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"SendGrid failed with status code: {response.StatusCode}");
            }
        }
    }
}
