using EmpowerID.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmpowerID.DAL.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmpowerIDDBContext _context;
        private readonly ILogger<EmployeeRepository> _logger;
        public EmployeeRepository(EmpowerIDDBContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IEnumerable<Employee> GetEmployees()
        {
            try
            {
                return _context.Employees
                    .Include(x => x.Department).AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error While Getting Employees");
                return new List<Employee>();
            }
        }
        public async Task<bool> AddEmployee(Employee employee)
        {
            try
            {
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error While Adding Employee");
                return false;
            }
        }
        public async Task<Employee?> GetEmployeeById(int id)
        {
            try
            {
                return await _context.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error While Adding Employee");
                return null;
            }

        }
        public async Task<bool> UpdateEmployee(Employee employee)
        {
            try
            {
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error While Updating Employees");
                return false;
            }
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    _logger.LogWarning("Employee with {Id} didn't found", id);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error While Updating Employees");
                return false;
            }
        }

        public IEnumerable<Department> GetDepartments()
        {
            try
            {
                return _context.Departments
                    .AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error While Getting Departments");
                return new List<Department>();
            }
        }
    }
}
