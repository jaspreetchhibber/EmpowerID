using System.ComponentModel.DataAnnotations;

namespace EmpowerID.DAL.Entities
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DepartmentName { get; set; } = string.Empty;
    }
}