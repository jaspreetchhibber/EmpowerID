using AutoMapper;
using EmpowerID.DAL.Entities;
using EmpowerID.DAL.Models;
using EmpowerID.DAL.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmpowerID.WebAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDto>().ReverseMap();
            });
            _mapper = config.CreateMapper();
        }
        // GET: api/<EmployeeController>
        [HttpGet("GetEmployees")]
        public IEnumerable<Employee> GetEmployees()
        {
            return _employeeRepository.GetEmployees();
        }

        // GET api/<EmployeeController>/5
        [HttpGet("GetEmployee/{id}")]
        public async Task<Employee?> Get(int id)
        {
            return await _employeeRepository.GetEmployeeById(id);
        }

        // POST api/<EmployeeController>
        [HttpPost("AddEmployee")]
        public async Task<bool> Add([FromBody] EmployeeDto employee)
        {
            var result = await _employeeRepository.AddEmployee(_mapper.Map<Employee>(employee));
            return result;
        }

        // PUT api/<EmployeeController>
        [HttpPost("UpdateEmployee")]
        public async Task<bool> Update([FromBody] EmployeeDto employee)
        {
            var result = await _employeeRepository.UpdateEmployee(_mapper.Map<Employee>(employee));
            return result;
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<bool> Delete(int id)
        {
            var result = await _employeeRepository.DeleteEmployee(id);
            return result;
        }

        [HttpGet("GetDepartments")]
        public IEnumerable<Department> GetDepartments()
        {
            return _employeeRepository.GetDepartments();
        }
    }
}
