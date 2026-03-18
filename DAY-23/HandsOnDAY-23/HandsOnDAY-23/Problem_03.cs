using System;
using System.Collections.Generic;
using System.Text;

namespace HandsOnDAY_23
{
    internal class Problem_03
    {
        static async Task VerifyPaymentAsync()
        {
            Console.WriteLine("Verifying Payment");
            await Task.Delay(2000);
            Console.WriteLine("Payment Verifed");
        }

        static async Task CheckInventoryAsync()
        {
            Console.WriteLine("\nChecking Inventory");
            await Task.Delay(2000);
            Console.WriteLine("Inventory Available");
        }

        static async Task ConfirmOrderAsync()
        { 
            Console.WriteLine("\nConfirming Order");
            await Task.Delay(2000);
            Console.WriteLine("Order Confirmed");
        }

        static async Task Main(string[] args)
        {
            await VerifyPaymentAsync();
            await CheckInventoryAsync();
            await ConfirmOrderAsync();

            Console.WriteLine("\nOrder Process Completed");
        }
    }
}
