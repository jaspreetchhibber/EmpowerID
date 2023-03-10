using EmpowerID.DAL.Entities;

namespace EmpowerID_10Mar.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DOB { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
