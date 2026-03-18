using System;
using System.Collections.Generic;
using System.Text;

namespace HandsOnDAY_23
{
    internal class Problem_04
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Price: ");
            double price = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Discount %: ");
            double discount = Convert.ToDouble(Console.ReadLine());

            // breakpoint
            double finalPrice = price - (price * discount / 100);

            Console.WriteLine("Final Price: " + finalPrice);
        }
    }
}
