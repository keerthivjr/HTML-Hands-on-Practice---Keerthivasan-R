using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDAY17
{
    internal class CalculatorConstructor
    {
        // Private data members
        private int num1;
        private int num2;

        // Constructor to initialize values
        public CalculatorConstructor(int a, int b)
        {
            num1 = a;
            num2 = b;
        }

        // Method for addition
        public void Add()
        {
            Console.WriteLine("Addition = " + (num1 + num2));
        }

        // Method for subtraction
        public void Subtract()
        {
            Console.WriteLine("Subtraction = " + (num1 - num2));
        }
    }

    class Program3
    {
        static void Main(string[] args)
        {
            Console.Write("Enter First Number: ");
            int a = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Second Number: ");
            int b = Convert.ToInt32(Console.ReadLine());

            // Object creation with constructor
            CalculatorConstructor calc = new CalculatorConstructor(a, b);

            calc.Add();
            calc.Subtract();

            Console.ReadLine();
        }
    }
}
