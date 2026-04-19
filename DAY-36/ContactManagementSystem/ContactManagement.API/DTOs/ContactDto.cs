namespace ContactManagement.API.DTOs
{
    public class ContactDto
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public long MobileNo { get; set; }
        public string Designation { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
    }

    public class CreateContactDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public long MobileNo { get; set; }
        public string Designation { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
    }

    public class UpdateContactDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public long MobileNo { get; set; }
        public string Designation { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
    }
}
