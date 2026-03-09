using System;

class StudentGradeEvaluator
{
    static void Main(string[] args)
    {
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();

        // Check if name is empty or only spaces
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Invalid name! Please enter a valid name.");
            return;
        }

        Console.Write("Enter Marks: ");
        int marks = Convert.ToInt32(Console.ReadLine());

        if (marks < 0 || marks > 100)
        {
            Console.WriteLine("Invalid marks! Please enter marks between 0 and 100.");
        }
        else if (marks >= 90)
        {
            Console.WriteLine("Student: " + name);
            Console.WriteLine("Grade: A");
        }
        else if (marks >= 75)
        {
            Console.WriteLine("Student: " + name);
            Console.WriteLine("Grade: B");
        }
        else if (marks >= 60)
        {
            Console.WriteLine("Student: " + name);
            Console.WriteLine("Grade: C");
        }
        else if (marks >= 50)
        {
            Console.WriteLine("Student: " + name);
            Console.WriteLine("Grade: D");
        }
        else
        {
            Console.WriteLine("Student: " + name);
            Console.WriteLine("Grade: Fail");
        }

        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();
    }
}