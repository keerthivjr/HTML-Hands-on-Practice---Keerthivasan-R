namespace EmployeeManagement.API.Services;
public class EmployeeService
{
    public double CalculateSalary(double basicSalary)
    {
        if (basicSalary < 0)
            throw new ArgumentException("Salary cannot be negative");

        return basicSalary + (0.2 * basicSalary);
    }
}