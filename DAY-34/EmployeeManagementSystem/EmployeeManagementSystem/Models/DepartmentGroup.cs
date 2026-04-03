using System.Collections.Generic;

namespace EmployeeManagementSystem.Models
{
    public class DepartmentGroup
    {
        public string? Department { get; set; }
        public int EmployeeCount { get; set; }
        public decimal AverageSalary { get; set; }
        public List<Employee> ?Employees { get; set; }
    }
}