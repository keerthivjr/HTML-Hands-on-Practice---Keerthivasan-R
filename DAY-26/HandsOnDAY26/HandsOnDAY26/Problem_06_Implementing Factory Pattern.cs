using System;

namespace HandsOnDAY26
{
    // 1. Interface
    public interface INotification
    {
        void Send(string message);
    }

    // 2. Concrete Classes

    public class EmailNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine("Email Sent: " + message);
        }
    }

    public class SMSNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine("SMS Sent: " + message);
        }
    }

    public class PushNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine("Push Notification Sent: " + message);
        }
    }

    // 3. Factory Class
    public class NotificationFactory
    {
        public INotification CreateNotification(string type)
        {
            switch (type.ToLower())
            {
                case "email":
                    return new EmailNotification();

                case "sms":
                    return new SMSNotification();

                case "push":
                    return new PushNotification();

                default:
                    throw new ArgumentException("Invalid notification type");
            }
        }
    }

    // Main Class
    internal class Problem_06_Implementing_Factory_Pattern
    {
        static void Main(string[] args)
        {
            NotificationFactory factory = new NotificationFactory();

            var email = factory.CreateNotification("email");
            email.Send("Welcome to our service!");

            var sms = factory.CreateNotification("sms");
            sms.Send("Your OTP is 123456");

            var push = factory.CreateNotification("push");
            push.Send("You have a new alert!");
        }
    }
}