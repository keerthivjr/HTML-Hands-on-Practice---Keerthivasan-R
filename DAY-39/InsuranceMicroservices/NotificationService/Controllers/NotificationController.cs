using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using NotificationService.Data;
using NotificationService.Models;
using NotificationService.Services;

namespace NotificationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;
        private readonly SmsService _smsService;

        public NotificationController(ApplicationDbContext context, EmailService emailService, SmsService smsService)
        {
            _context = context;
            _emailService = emailService;
            _smsService = smsService;
        }

        // POST: api/notification/send
        [HttpPost("send")]
        public async Task<IActionResult> SendNotification(SendNotificationRequest request)
        {
            var notification = new Notification
            {
                NotificationId = $"NOT{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                CustomerId = request.CustomerId,
                CustomerEmail = request.CustomerEmail,
                CustomerPhone = request.CustomerPhone,
                Type = request.Type,
                Subject = request.Subject,
                Message = request.Message,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            // Send notification based on type
            bool success = false;
            string errorMessage = null;

            try
            {
                if (request.Type == "Email")
                {
                    success = await _emailService.SendEmailAsync(request.CustomerEmail, request.Subject, request.Message);
                }
                else if (request.Type == "SMS")
                {
                    success = await _smsService.SendSmsAsync(request.CustomerPhone, request.Message);
                }

                if (success)
                {
                    notification.Status = "Sent";
                    notification.SentAt = DateTime.UtcNow;
                }
                else
                {
                    notification.Status = "Failed";
                    notification.ErrorMessage = "Failed to send notification";
                }
            }
            catch (Exception ex)
            {
                notification.Status = "Failed";
                notification.ErrorMessage = ex.Message;
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = success ? "Notification sent successfully" : "Failed to send notification",
                notificationId = notification.NotificationId,
                status = notification.Status
            });
        }

        // GET: api/notification/customer/{customerId}
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCustomerNotifications(int customerId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.CustomerId == customerId)
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new NotificationResponse
                {
                    Id = n.Id,
                    NotificationId = n.NotificationId,
                    CustomerId = n.CustomerId,
                    Type = n.Type,
                    Subject = n.Subject,
                    Message = n.Message,
                    Status = n.Status,
                    CreatedAt = n.CreatedAt,
                    SentAt = n.SentAt
                })
                .ToListAsync();

            return Ok(notifications);
        }

        // GET: api/notification/all
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllNotifications()
        {
            var notifications = await _context.Notifications
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return Ok(notifications);
        }
    }
}