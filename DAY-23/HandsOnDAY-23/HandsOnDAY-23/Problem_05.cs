using System;
using System.Diagnostics;
using System.IO;

namespace HandsOnDAY_23
{
    internal class Problem_05
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new TextWriterTraceListener(@"C:\Users\keerthi\OneDrive\Desktop\upGrad\DAY-23\traceLog.txt"));
            Trace.AutoFlush = true;

            Trace.WriteLine("Order Processing Started");

            ValidateOrder();
            ProcessPayment();
            UpdateInventory();
            GenerateInvoice();

            Trace.WriteLine("Order Processing Completed");

            Console.WriteLine("Tracing completed. Check traceLog.txt");
        }

        static void ValidateOrder()
        {
            Trace.TraceInformation("Validating Order...");
        }

        static void ProcessPayment()
        {
            Trace.TraceInformation("Processing Payment...");
        }

        static void UpdateInventory()
        {
            Trace.TraceInformation("Updating Inventory...");
        }

        static void GenerateInvoice()
        {
            Trace.TraceInformation("Generating Invoice...");
        }
    }
}