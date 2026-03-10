using System;

namespace HandsOnDAY17
{
    internal class Calculator
    {
        // Private data members
        private int num1;
        private int num2;

        // Public method to set values
        public void SetValues(int a, int b)
        {
            num1 = a;
            num2 = b;
        }

        // Method for addition
        public int Add()
        {
            return num1 + num2;
        }

        // Method for subtraction
        public int Subtract()
        {
            return num1 - num2;
        }

        // Method to display values
        public void DisplayNumbers()
        {
            Console.WriteLine("First Number: " + num1);
            Console.WriteLine("Second Number: " + num2);
        }
    }

    class Program1
    {
        static void Main(string[] args)
        {
            Calculator calc = new Calculator();

            Console.Write("Enter first number: ");
            int a = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter second number: ");
            int b = Convert.ToInt32(Console.ReadLine());

            // Setting values
            calc.SetValues(a, b);

            // Display numbers
            calc.DisplayNumbers();

            // Perform operations
            Console.WriteLine("Addition = " + calc.Add());
            Console.WriteLine("Subtraction = " + calc.Subtract());
        }
    }
}