using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDAY26
{
    class RepoStudent1 // holds data
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public int Marks { get; set; }

        public RepoStudent1(int id, string name, int marks   )
        {
            Id = id;
            Name = name;
            Marks = marks;
        }
    }

    class StudentRepository1 // manages data
    { 
        private List<RepoStudent1> students = new List<RepoStudent1>();

        public void AddStudent(RepoStudent1 student)
        { 
            students.Add(student);
        }

        public List<RepoStudent1> GetAllStudents()
        { 
            return students;
        }
    }

    class ReportGenerator // generates report
    {
        public void GenerateReport(List<RepoStudent1> students)
        {
            Console.WriteLine("----- Student Report -----");

            foreach (var student in students)
            {
                string grade = GetGrade(student.Marks);

                Console.WriteLine($"ID: {student.Id}");
                Console.WriteLine($"Name: {student.Name}");
                Console.WriteLine($"Marks: {student.Marks}");
                Console.WriteLine($"Grade: {grade}");
                Console.WriteLine("-------------------------");
            }
        }

        private string GetGrade(int marks)
        {
            if (marks >= 90)
            {
                return "A";
            }
            else if (marks >= 75)
            {
                return "B";
            }
            else if (marks >= 50)
            {
                return "C";
            }
            else
            {
                return "Fail";
            }
        }
    }
    internal class Problem_01_SRP // SRP – Single Responsibility Principle
    {
        static void Main(string[] args) 
        { 
            StudentRepository1 repo = new StudentRepository1();

            //adding students
            repo.AddStudent(new RepoStudent1(1, "Arun", 92));
            repo.AddStudent(new RepoStudent1(2, "Divya", 76));
            repo.AddStudent(new RepoStudent1(3, "Kiran", 45));

            //Geberate Report
            ReportGenerator report = new ReportGenerator();
            report.GenerateReport(repo.GetAllStudents());
        }
    }
}
