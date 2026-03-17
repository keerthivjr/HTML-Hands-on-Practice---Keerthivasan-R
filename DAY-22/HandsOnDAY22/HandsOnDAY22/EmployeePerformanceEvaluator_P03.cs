using System;
using System.Collections.Generic;
using System.Text;

namespace HandsOnDAY22
{
    internal class EmployeePerformanceEvaluator_P03
    {
        static (double sales, int rating) GetPerformanceData(double sales, int rating)
        {
            return (sales, rating);
        }
        static void Main(string[] args) {

            try
            {
                Console.Write("Enter Employee Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter Sales Amount: ");
                double sales = double.Parse(Console.ReadLine());

                Console.Write("Enter Rating (1-5): ");
                int rating = int.Parse(Console.ReadLine());

                var result = GetPerformanceData(sales, rating);

                //Pattern matching switch expression
                // This checks both values in the tuple simultaneously
                string performanceCategory = result switch
                {
                    ( >= 100000, >= 4) => "High Performer",
                    ( >= 50000, >= 3) => "Average Performer",
                    _ => "Needs Improvement" // The discard symbol '_' handles all other cases
                };

                Console.WriteLine("\n===== RESULT =====");
                Console.WriteLine($"Employee Name: {name}");
                Console.WriteLine($"Sales Amount: {result.sales}");
                Console.WriteLine($"Rating: {result.rating}");

                Console.WriteLine($"Performance: {performanceCategory}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter valid numeric values for Sales and Rating.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
