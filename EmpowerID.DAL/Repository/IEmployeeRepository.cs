using EmpowerID.DAL.Entities;

namespace EmpowerID.DAL.Repository
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees();
        Task<bool> AddEmployee(Employee employee);
        Task<Employee?> GetEmployeeById(int id);
        Task<bool> UpdateEmployee(Employee employee);
        Task<bool> DeleteEmployee(int id);
        IEnumerable<Department> GetDepartments();
    }
}
