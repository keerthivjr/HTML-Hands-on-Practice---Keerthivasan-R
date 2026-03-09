using System;
using System.Diagnostics;

class EmployeeBonusCalculator {

    static void Main(string[] args) {

        Console.Write("Enter Name: ");
        String name = Console.ReadLine();

        Console.Write("Enter Salary: ");
        double salary = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter YOE: ");
        int experience = Convert.ToInt32(Console.ReadLine());

        double bonusPercentage;

        if (experience < 2)
        {
            bonusPercentage = 0.05;
        }
        else if (experience <= 5)
        {
            bonusPercentage = 0.10;
        }
        else {
            bonusPercentage = 0.15;
        }

        double bonus = salary * bonusPercentage;

        //ternary operator example
        double finalSalary = bonus > 0 ? salary + bonus : salary;


        Console.WriteLine("\nEmployee: " + name);
        Console.WriteLine("Bonus: " + bonus.ToString("F2"));
        Console.WriteLine("Final Salary: " + finalSalary.ToString("F2"));
    }
}