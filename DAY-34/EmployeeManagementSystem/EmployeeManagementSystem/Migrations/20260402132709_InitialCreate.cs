using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    JobStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "Department", "Email", "HireDate", "JobStatus", "Name", "PhoneNumber", "Salary" },
                values: new object[,]
                {
                    { 1, "123 Main St", "IT", "john@company.com", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active", "John Doe", "1234567890", 75000m },
                    { 2, "456 Oak Ave", "HR", "jane@company.com", new DateTime(2023, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active", "Jane Smith", "0987654321", 65000m },
                    { 3, "789 Pine Rd", "IT", "bob@company.com", new DateTime(2022, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active", "Bob Johnson", "5555555555", 85000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
