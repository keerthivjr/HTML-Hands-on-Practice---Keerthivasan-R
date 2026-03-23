using System;
using System.Collections.Generic;
using System.Linq;

namespace HandsOnDAY26
{
    // Entity Class
    public class RepoStudent
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Course { get; set; }

        public RepoStudent(int id, string name, string course)
        {
            StudentId = id;
            StudentName = name;
            Course = course;
        }
    }

    // Repository Interface
    public interface IRepoStudentRepository
    {
        void AddStudent(RepoStudent student);
        List<RepoStudent> GetAllStudents();
        RepoStudent GetStudentById(int id);
        void DeleteStudent(int id);
    }

    // Repository Implementation
    public class RepoStudentRepository : IRepoStudentRepository
    {
        private List<RepoStudent> students = new List<RepoStudent>();

        public void AddStudent(RepoStudent student)
        {
            students.Add(student);
        }

        public List<RepoStudent> GetAllStudents()
        {
            return students;
        }

        public RepoStudent GetStudentById(int id)
        {
            return students.FirstOrDefault(s => s.StudentId == id);
        }

        public void DeleteStudent(int id)
        {
            var student = GetStudentById(id);
            if (student != null)
            {
                students.Remove(student);
            }
        }
    }

    // Main Class
    internal class Implementing_Repository_Pattern
    {
        static void Main(string[] args)
        {
            // ✅ Correct: Use implementation class
            IRepoStudentRepository repo = new RepoStudentRepository();

            // Adding Students
            repo.AddStudent(new RepoStudent(1, "Arun", "Java"));
            repo.AddStudent(new RepoStudent(2, "Divya", "Python"));
            repo.AddStudent(new RepoStudent(3, "Kiran", "C#"));

            // View All Students
            Console.WriteLine("All Students:");
            foreach (var s in repo.GetAllStudents())
            {
                Console.WriteLine($"ID: {s.StudentId}, Name: {s.StudentName}, Course: {s.Course}");
            }

            Console.WriteLine();

            // Find Student by ID
            Console.WriteLine("Search Student with ID 2:");
            var student = repo.GetStudentById(2);
            if (student != null)
            {
                Console.WriteLine($"Found: {student.StudentName}, Course: {student.Course}");
            }
            else
            {
                Console.WriteLine("Student not found");
            }

            Console.WriteLine();

            // Delete Student
            Console.WriteLine("Deleting Student with ID 1...");
            repo.DeleteStudent(1);

            Console.WriteLine("Students After Deletion:");
            foreach (var s in repo.GetAllStudents())
            {
                Console.WriteLine($"ID: {s.StudentId}, Name: {s.StudentName}, Course: {s.Course}");
            }
        }
    }
}