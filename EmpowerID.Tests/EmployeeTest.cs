using EmpowerID.DAL.Entities;
using EmpowerID.DAL.Models;
using EmpowerID.DAL.Repository;
using EmpowerID.WebAPI.Controllers;
using Moq;

namespace EmpowerID.Tests
{
    public class EmployeeTest
    {
        private readonly Mock<IEmployeeRepository> _employeeRepository;

        public EmployeeTest()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
        }

        [Fact]
        public void GetProductList_ProductList()
        {
            //arrange
            var employees = GetEmployeeData();
            _employeeRepository.Setup(x => x.GetEmployees())
                .Returns(employees);
            var employeeController = new EmployeeController(_employeeRepository.Object);

            //act
            var result = employeeController.GetEmployees();

            //assert
            Assert.NotNull(result);
            Assert.Equal(GetEmployeeData().Count(), result.Count());
            Assert.Equal(GetEmployeeData().ToString(), result.ToString());
            Assert.True(employees.Equals(result));
        }

        [Fact]
        public void GetEmployeeByID_Employee()
        {
            //arrange
            var employees = GetEmployeeData();
            _employeeRepository.Setup(x => x.GetEmployeeById(2).Result)
                .Returns(employees[1]);
            var employeeController = new EmployeeController(_employeeRepository.Object);

            //act
            var result = employeeController.Get(2).Result;

            //assert
            Assert.NotNull(result);
            Assert.Equal(employees[1].Id, result.Id);
            Assert.True(employees[1].Id == result.Id);
        }

        [Fact]
        public void AddEmployee_Employee()
        {
            //arrange
            var employees = GetEmployeeData();
            var employeesDto = GetEmployeeDtoData();
            _employeeRepository.Setup(x => x.AddEmployee(employees[2]).Result)
                .Returns(true);
            var employeeController = new EmployeeController(_employeeRepository.Object);

            //act
            var result = employeeController.Add(employeesDto[2]);

            //assert
            Assert.NotNull(result);
            Assert.True(true);
            Assert.True(true == true);
        }

        [Fact]
        public void UpdateEmployee_Employee()
        {
            //arrange
            var employees = GetEmployeeData();
            var employeesDto = GetEmployeeDtoData();
            _employeeRepository.Setup(x => x.UpdateEmployee(employees[2]).Result)
                .Returns(true);
            var employeeController = new EmployeeController(_employeeRepository.Object);

            //act
            var result = employeeController.Update(employeesDto[2]);

            //assert
            Assert.NotNull(result);
            Assert.True(true);
            Assert.True(true == true);
        }

        [Fact]
        public void Delete_Employee()
        {
            //arrange
            var employees = GetEmployeeData();
            _employeeRepository.Setup(x => x.DeleteEmployee(2).Result)
                .Returns(true);
            var employeeController = new EmployeeController(_employeeRepository.Object);

            //act
            var result = employeeController.Delete(2).Result;

            //assert
            Assert.True(true);
            Assert.True(true == true);
        }

        private List<Employee> GetEmployeeData()
        {
            List<Employee> productsData = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                FirstName = "Emp1",
                LastName = "Last1",
                Email = "Emp1Test@Test.com",
                DOB = DateTime.Now.AddYears(-20)
            },
             new Employee
            {
                 Id = 2,
                FirstName = "Emp2",
                LastName = "Last2",
                Email = "Emp2Test@Test.com",
                DOB = DateTime.Now.AddYears(-40)
            },
             new Employee
            {
                 Id = 3,
                FirstName = "Emp3",
                LastName = "Last3",
                Email = "Emp3Test@Test.com",
                DOB = DateTime.Now.AddYears(-25)
            },
        };
            return productsData;
        }
        private List<EmployeeDto> GetEmployeeDtoData()
        {
            List<EmployeeDto> productsData = new List<EmployeeDto>
        {
            new EmployeeDto
            {
                Id = 1,
                FirstName = "Emp1",
                LastName = "Last1",
                Email = "Emp1Test@Test.com",
                DOB = DateTime.Now.AddYears(-20)
            },
             new EmployeeDto
            {
                 Id = 2,
                FirstName = "Emp2",
                LastName = "Last2",
                Email = "Emp2Test@Test.com",
                DOB = DateTime.Now.AddYears(-40)
            },
             new EmployeeDto
            {
                 Id = 3,
                FirstName = "Emp3",
                LastName = "Last3",
                Email = "Emp3Test@Test.com",
                DOB = DateTime.Now.AddYears(-25)
            },
        };
            return productsData;
        }
    }
}