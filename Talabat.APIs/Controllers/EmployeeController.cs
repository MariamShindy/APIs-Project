using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Employees;
using Talabat.Core.Repsitories.Contract;
using Talabat.Core.Specifications.Employee_Specs;

namespace Talabat.APIs.Controllers
{
    public class EmployeeController : BaseApiController
	{
		private readonly IGenericRepository<Employee> _employeeRepository;

		public EmployeeController(IGenericRepository<Employee> employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
		{
			var spec = new EmployeeWithDepartmentSpecifications();
			var employees = await _employeeRepository.GetAllWithSpecAsync(spec);
			return Ok(employees);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Employee>> GetEmployee (int id)
		{
			var spec = new EmployeeWithDepartmentSpecifications(id);
			var employee = await _employeeRepository.GetWithSpecAsync(spec);
			if (employee is null)
				return NotFound(new ApiResponse(404));
			return Ok(employee);
		}

    }
}
