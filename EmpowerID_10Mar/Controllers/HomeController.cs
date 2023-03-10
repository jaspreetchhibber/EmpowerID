using AutoMapper;
using EmpowerID.DAL.Entities;
using EmpowerID.DAL.Models;
using EmpowerID_10Mar.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace EmpowerID_10Mar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly string apiBaseUrl;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            apiBaseUrl = _configuration.GetValue<string>("WebAPIBaseUrl");
            _logger = logger;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeViewModel>().ReverseMap();
                cfg.CreateMap<EmployeeDto, EmployeeViewModel>().ReverseMap();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "Employee/GetEmployees";
                using (var response = await client.GetAsync(endpoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        if (responseString != null && responseString != string.Empty)
                        {
                            var employees = JsonConvert.DeserializeObject<List<Employee>>(responseString);
                            var employeeList = _mapper.Map<List<EmployeeViewModel>>(employees);
                            return View(employeeList);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
            }
            return View();
        }
        public async Task<IActionResult> AddEmployee(int? id)
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            using (var client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "Employee/GetDepartments";
                using (var response = await client.GetAsync(endpoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        if (responseString != null && responseString != string.Empty)
                        {
                            var departments = JsonConvert.DeserializeObject<List<Department>>(responseString);
                            ViewBag.Deparments = departments;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
            }

            if (id != null)
            {
                using (var client = new HttpClient())
                {
                    string endpoint = apiBaseUrl + "Employee/GetEmployee/" + id;
                    using (var response = await client.GetAsync(endpoint))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var responseString = await response.Content.ReadAsStringAsync();
                            if (responseString != null && responseString != string.Empty)
                            {
                                var empData = JsonConvert.DeserializeObject<Employee>(responseString);
                                employeeViewModel = _mapper.Map<EmployeeViewModel>(empData);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        }
                    }
                }
            }

            return View(employeeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeViewModel employeeViewModel)
        {

            var employeeData = _mapper.Map<EmployeeDto>(employeeViewModel);
            HttpContent body = new StringContent(JsonConvert.SerializeObject(employeeData), Encoding.UTF8, "application/json");


            using (var client = new HttpClient())
            {
                string endpoint = "";
                if (employeeViewModel.Id == 0)
                {
                    endpoint = apiBaseUrl + "Employee/AddEmployee";
                }
                else
                {
                    endpoint = apiBaseUrl + "Employee/UpdateEmployee";
                }

                using (var response = await client.PostAsync(endpoint, body))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        if (responseString != null && responseString != string.Empty)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
            }

            return View(employeeViewModel);
        }
        [HttpDelete]
        public async Task<JsonResult> DeleteEmployee(int id)
        {
            using (var client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "Employee/DeleteEmployee/" + id;

                using (var response = await client.DeleteAsync(endpoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        if (responseString != null && responseString != string.Empty)
                        {
                            return Json(new { redirectUrl = "Index" });
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
            }
            return new JsonResult(null);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}