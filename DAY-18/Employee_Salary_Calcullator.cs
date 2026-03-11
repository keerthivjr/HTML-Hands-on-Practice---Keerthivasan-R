using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDAY18
{
    class Employee {
        // properties
        public string name { get; set; }
        public double baseSalary { get; set; }
        //virtual method 
        public  virtual double CalculateSalary() { 
            return baseSalary;
        }
    }
    class Manager : Employee {
        //override method
        public override double CalculateSalary() {
            return baseSalary + (baseSalary * 0.20);
        }
    }
    class Developer : Employee {
        //override method
        public override double CalculateSalary()
        {
            return baseSalary + (baseSalary * 0.10);
        }
    }
    internal class Employee_Salary_Calcullator
    {
        static void Main(string[] args) { 
            
            Console.Write("Enter the base salary: ");
            double baseSalary = Convert.ToDouble(Console.ReadLine());

            //polymorphism using base class reference
            Employee manager = new Manager();
            manager.baseSalary = baseSalary;

            Employee developer = new Developer();
            developer.baseSalary = baseSalary;

            Console.WriteLine("Manager Salary: " + manager.CalculateSalary());
            Console.WriteLine("Developer Salary: " + developer.CalculateSalary());
           
        }
    }
}
