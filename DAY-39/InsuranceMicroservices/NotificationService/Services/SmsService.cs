namespace NotificationService.Services
{
    public class SmsService
    {
        // Simulate SMS sending (in production, use Twilio, Vonage, etc.)
        public async Task<bool> SendSmsAsync(string phoneNumber, string message)
        {
            // Simulate network delay
            await Task.Delay(300);

            // Log to console
            Console.WriteLine($"📱 SMS SENT:");
            Console.WriteLine($"   To: {phoneNumber}");
            Console.WriteLine($"   Message: {message}");
            Console.WriteLine($"   Time: {DateTime.UtcNow}");

            // Return true for successful send simulation
            return true;
        }
    }
}