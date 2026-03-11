using System;

namespace HandsOnDAY18
{
    class Student
    {
        public double CalculateAverage(int m1, int m2, int m3)
        {
            return (m1 + m2 + m3) / 3.0;
        }

        public string CalculateGrade(double average)
        {
            if (average >= 80)
                return "A";
            else if (average >= 65)
                return "B";
            else if (average >= 50)
                return "C";
            else if (average >= 35)
                return "D";
            else
                return "F";
        }
    }

    class StudentGradeCalculator
    {
        static void Main(string[] args)
        {
            Student student = new Student();

            Console.Write("Enter the Mark1: ");
            int m1 = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the Mark2: ");
            int m2 = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the Mark3: ");
            int m3 = Convert.ToInt32(Console.ReadLine());

            double average = student.CalculateAverage(m1, m2, m3);

            string grade = student.CalculateGrade(average);

            Console.WriteLine("Average = " + average + ", Grade = " + grade);
        }
    }
}