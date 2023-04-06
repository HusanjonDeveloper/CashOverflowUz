//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using CashOverflowUz.Models.Salaries;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflowUz.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class SalariesController : RESTFulController
	{
		private readonly ISalaryService salaryService;

		public SalariesController(ISalaryService salaryService) =>
			this.salaryService = salaryService;

		[HttpPost]
		public async ValueTask<ActionResult<Salary>> PostSalaryAsync(Salary salary)
		{
			try
			{
				Salary addedSalary = await this.salaryService.AddSalaryAsync(salary);

				return Created(addedSalary);
			}
			catch (SalaryValidationException salaryValidationException)
			{
				return BadRequest(salaryValidationException.InnerException);
			}
			catch (SalaryDependencyValidationException salaryDependencyValidationException)
				when (salaryDependencyValidationException.InnerException is AlreadyExistsSalaryException)
			{
				return Conflict(salaryDependencyValidationException.InnerException);
			}
			catch (SalaryDependencyException salaryDependencyException)
			{
				return InternalServerError(salaryDependencyException.InnerException);
			}
			catch (SalaryServiceException salaryServiceException)
			{
				return InternalServerError(salaryServiceException.InnerException);
			}
		}


		[HttpGet]
		public ActionResult<IQueryable<Salary>> GetAllSalaries()
		{
			try
			{
				IQueryable<Salary> allSalaries = this.salaryService.RetrieveAllSalaries();

				return Ok(allSalaries);
			}
			catch (SalaryDependencyException salaryDependencyException)
			{
				return InternalServerError(salaryDependencyException.InnerException);
			}
			catch (SalaryServiceException salaryServiceException)
			{
				return InternalServerError(salaryServiceException.InnerException);
			}
		}
	}
}
