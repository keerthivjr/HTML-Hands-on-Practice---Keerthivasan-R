using System;

namespace HandsOnDAY26
{
    // 1. Small Interfaces

    public interface IPrinter
    {
        void Print();
    }

    public interface IScanner
    {
        void Scan();
    }

    public interface IFax
    {
        void Fax();
    }

    // 2. Basic Printer (only Print)
    public class BasicPrinter : IPrinter
    {
        public void Print()
        {
            Console.WriteLine("Basic Printer: Printing document...");
        }
    }

    // 3. Advanced Printer (Print + Scan + Fax)
    public class AdvancedPrinter : IPrinter, IScanner, IFax
    {
        public void Print()
        {
            Console.WriteLine("Advanced Printer: Printing document...");
        }

        public void Scan()
        {
            Console.WriteLine("Advanced Printer: Scanning document...");
        }

        public void Fax()
        {
            Console.WriteLine("Advanced Printer: Sending fax...");
        }
    }

    // Main Class
    internal class Problem_04_ISP
    {
        static void Main(string[] args)
        {
            // Basic Printer
            IPrinter basic = new BasicPrinter();
            basic.Print();

            Console.WriteLine();

            // Advanced Printer
            AdvancedPrinter advanced = new AdvancedPrinter();
            advanced.Print();
            advanced.Scan();
            advanced.Fax();
        }
    }
}