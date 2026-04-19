using NUnit.Framework;
using EmployeeManagement.API.Services;

[TestFixture]
public class EmployeeServiceTests
{
    private EmployeeService _service;

    [SetUp]
    public void Setup()
    {
        _service = new EmployeeService();
    }

    [Test]
    public void CalculateSalary_ValidInput_ReturnsCorrectSalary()
    {
        var result = _service.CalculateSalary(10000);
        Assert.AreEqual(12000, result);
    }

    [Test]
    public void CalculateSalary_NegativeInput_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => _service.CalculateSalary(-5000));
    }
}