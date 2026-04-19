using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsuranceClaimAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PolicyHolders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PolicyStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PolicyEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyHolders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PolicyHolderId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncidentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IncidentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentUrls = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProcessedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsuranceClaims_PolicyHolders_PolicyHolderId",
                        column: x => x.PolicyHolderId,
                        principalTable: "PolicyHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClaimDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClaimDocuments_InsuranceClaims_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "InsuranceClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PolicyHolders",
                columns: new[] { "Id", "Address", "CoverageAmount", "CreatedAt", "Email", "FirstName", "IsActive", "LastName", "PhoneNumber", "PolicyEndDate", "PolicyNumber", "PolicyStartDate", "PolicyType" },
                values: new object[] { 1, "123 Main St, Anytown, AN 12345", 500000m, new DateTime(2026, 4, 14, 12, 1, 52, 460, DateTimeKind.Utc).AddTicks(2479), "john.smith@example.com", "John", true, "Smith", "555-0101", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "POL-2024-001", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Health" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 14, 12, 1, 52, 460, DateTimeKind.Utc).AddTicks(2334), "admin@insurance.com", true, "admin123", null, null, "Admin", "admin" },
                    { 2, new DateTime(2026, 4, 14, 12, 1, 52, 460, DateTimeKind.Utc).AddTicks(2336), "processor@insurance.com", true, "processor123", null, null, "ClaimsProcessor", "claimsprocessor" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimDocuments_ClaimId",
                table: "ClaimDocuments",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceClaims_ClaimNumber",
                table: "InsuranceClaims",
                column: "ClaimNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceClaims_PolicyHolderId",
                table: "InsuranceClaims",
                column: "PolicyHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyHolders_PolicyNumber",
                table: "PolicyHolders",
                column: "PolicyNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimDocuments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "InsuranceClaims");

            migrationBuilder.DropTable(
                name: "PolicyHolders");
        }
    }
}
