namespace EmployeeManagementSystem.Models
{
    public class StatisticsViewModel
    {
        public int TotalEmployees { get; set; }
        public decimal OverallAverageSalary { get; set; }
        public Employee? HighestPaidEmployee { get; set; }
        public List<DepartmentGroup>? DepartmentGroups { get; set; }
    }
}