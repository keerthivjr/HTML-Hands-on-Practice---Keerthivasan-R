using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDAY17
{
    internal class CalculatorApp
    {
        public int Add(int a, int b)
        {
            return a + b;
        }


        public int Subtract(int a, int b)
        {
            return a - b;
        }
    }

    class Program2
    {
        static void Main(string[] args)
        {
            CalculatorApp calc = new CalculatorApp();

            Console.Write("Enter first number: ");
            int num1 = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter second number: ");
            int num2 = Convert.ToInt32(Console.ReadLine());

            int addResult = calc.Add(num1, num2);
            int subResult = calc.Subtract(num1, num2);

            Console.WriteLine("Addition = " + addResult);
            Console.WriteLine("Subtraction = " + subResult);
        }
    }
}
