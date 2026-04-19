namespace NotificationService.Services
{
    public class EmailService
    {
        // Simulate email sending (in production, use SMTP like SendGrid, AWS SES, etc.)
        public async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            // Simulate network delay
            await Task.Delay(500);

            // Log to console (in real implementation, send actual email)
            Console.WriteLine($"📧 EMAIL SENT:");
            Console.WriteLine($"   To: {to}");
            Console.WriteLine($"   Subject: {subject}");
            Console.WriteLine($"   Body: {body}");
            Console.WriteLine($"   Time: {DateTime.UtcNow}");

            // Return true for successful send simulation
            return true;
        }
    }
}