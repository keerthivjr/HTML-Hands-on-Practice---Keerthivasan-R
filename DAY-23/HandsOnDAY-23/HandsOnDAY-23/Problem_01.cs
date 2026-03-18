using System;
using System.Collections.Generic;
using System.Text;

namespace HandsOnDAY_23
{
    internal class Problem_01
    {
        static void GenerateSalesReport()
        {
            Console.WriteLine("Sales report started...");
            Thread.Sleep(2000);
            Console.WriteLine("\nSales report completed");
        }

        static void GenerateInventoryReport()
        {
            Console.WriteLine("Inventory report started");
            Thread.Sleep(3000);
            Console.WriteLine("Inventory report completed");
        }

        static void GenerateCustomerReport()
        {
            Console.WriteLine("Customer report started");
            Thread.Sleep(2500);
            Console.WriteLine("Customer report completed");
        }

        static void Main(string[] args)
        {
            Task t1 = Task.Run(() => GenerateSalesReport());
            Task t2 = Task.Run(() => GenerateInventoryReport());
            Task t3 = Task.Run(() => GenerateCustomerReport());

            Task.WaitAll(t1, t2, t3);

            Console.WriteLine("\nAll reports are generated successfully!");
        }

    }
}
