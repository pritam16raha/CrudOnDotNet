using CrudOnMySQL.Data;
using CrudOnMySQL.Models;
using CrudOnMySQL.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudOnMySQL.Controllers
{
    [Route("api/[controller]")] //localhost:port/api/[controller]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //getting all data
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            //var allEmployees = dbContext.Employees.ToList();
            //return Ok(allEmployees);
            return Ok(dbContext.Employees.ToList());
        }

        //get single user data
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var userInDb = dbContext.Employees.Find(id);
            if (userInDb is null)
            {
                return NotFound();
            }
            return Ok(userInDb);
        }

        //posting single data
        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDTO addEmployeeDTO) //DTO -> Data Transfer Object
        {
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDTO.Name,
                Email = addEmployeeDTO.Email,
                Phone = addEmployeeDTO.Phone,
                Salary = addEmployeeDTO.Salary,
                Photo = addEmployeeDTO.Photo,
            };
            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();
            return Ok(employeeEntity);
        }

        //update single user
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult updateEmployee(Guid id, UpdateEmployeeDTO updateEmployeeDTO)
        {
            var userInDb = dbContext.Employees.Find(id);
            if (userInDb is null)
            {
                return NotFound();
            }

            userInDb.Name = updateEmployeeDTO.Name;
            userInDb.Email = updateEmployeeDTO.Email;
            userInDb.Phone = updateEmployeeDTO.Phone;
            userInDb.Salary = updateEmployeeDTO.Salary;
            userInDb.Photo = updateEmployeeDTO.Photo;

            dbContext.SaveChanges();
            return Ok(userInDb);
        }

        //delete single user
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult deleteEmployee(Guid id)
        {
            var userInDb = dbContext.Employees.Find(id);
            if (userInDb is null)
            {
                return NotFound();
            }
            dbContext.Employees.Remove(userInDb);
            dbContext.SaveChanges();
            return Ok(userInDb);
        }
    }
}
