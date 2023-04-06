//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using CashOverflowUz.Models.job;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;
using CashOverflowUz.Services.Jobs;
using CashOverflowUz.Models.jobs.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CashOverflowUz.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class JobsController : RESTFulController
	{
		private readonly IJobService jobService;

		public JobsController(IJobService jobService) =>
			this.jobService = jobService;

		[HttpPost]
		public async ValueTask<ActionResult<Job>> PostJobAsync(Job job)
		{
			try
			{
				return await this.jobService.AddJobAsync(job);
			}
			catch (JobValidationException jobValidationException)
			{
				return BadRequest(jobValidationException.InnerException);
			}
			catch (JobDependencyValidationException jobDependencyValidationException)
				  when (jobDependencyValidationException.InnerException is AlreadyExistsJobException)
			{
				return Conflict(jobDependencyValidationException.InnerException);
			}
			catch (JobDependencyValidationException jobDependencyValidationException)
			{
				return BadRequest(jobDependencyValidationException.InnerException);
			}
			catch (JobDependencyException jobDependencyException)
			{
				return InternalServerError(jobDependencyException.InnerException);
			}
			catch (JobServiceException jobServiceException)
			{
				return InternalServerError(jobServiceException.InnerException);
			}
		}

		[HttpGet]
		public ActionResult<IQueryable<Job>> GetAllJobs()
		{
			try
			{
				IQueryable<Job> allJobs = this.jobService.RetrieveAllJobs();

				return Ok(allJobs);
			}
			catch (JobDependencyException jobDependencyException)
			{
				return InternalServerError(jobDependencyException);
			}
			catch (JobServiceException jobServiceException)
			{
				return InternalServerError(jobServiceException);
			}
		}

		[HttpGet("{jobId}")]
		public async ValueTask<ActionResult<Job>> GetJobByIdAsync(Guid jobId)
		{
			try
			{
				return await this.jobService.RetrieveJobByIdAsync(jobId);
			}
			catch (JobDependencyException jobDependencyException)
			{
				return InternalServerError(jobDependencyException.InnerException);
			}
			catch (JobValidationException jobValidationException)
				when (jobValidationException.InnerException is InvalidJobException)
			{
				return BadRequest(jobValidationException.InnerException);
			}
			catch (JobValidationException jobValidationException)
				when (jobValidationException.InnerException is NotFoundJobException)
			{
				return NotFound(jobValidationException.InnerException);
			}
			catch (JobServiceException jobServiceException)
			{
				return InternalServerError(jobServiceException.InnerException);
			}
		}

		[HttpPut]
		public async ValueTask<ActionResult<Job>> PutJobAsync(Job job)
		{
			try
			{
				Job modifiedJob = await this.jobService.ModifyJobAsync(job);

				return Ok(modifiedJob);
			}
			catch (JobValidationException jobValidationException)
				when (jobValidationException.InnerException is NotFoundJobException)
			{
				return NotFound(jobValidationException.InnerException);
			}
			catch (JobValidationException jobValidationException)
			{
				return BadRequest(jobValidationException.InnerException);
			}
			catch (JobDependencyValidationException jobDependencyValidationException)
			{
				return BadRequest(jobDependencyValidationException.InnerException);
			}
			catch (JobDependencyException jobDependencyException)
			{
				return InternalServerError(jobDependencyException.InnerException);
			}
			catch (JobServiceException jobServiceException)
			{
				return InternalServerError(jobServiceException.InnerException);
			}
		}

		[HttpDelete("{jobId}")]
		public async ValueTask<ActionResult<Job>> DeleteJobByIdAsync(Guid jobId)
		{
			try
			{
				Job deletedJob =
					await this.jobService.RemoveJobByIdAsync(jobId);

				return Ok(deletedJob);
			}
			catch (JobValidationException jobValidationException)
				when (jobValidationException.InnerException is NotFoundJobException)
			{
				return NotFound(jobValidationException.InnerException);
			}
			catch (JobValidationException jobValidationException)
			{
				return BadRequest(jobValidationException.InnerException);
			}
			catch (JobDependencyValidationException jobDependencyValidationException)
				when (jobDependencyValidationException.InnerException is LockedJobException)
			{
				return Locked(jobDependencyValidationException.InnerException);
			}
			catch (JobDependencyValidationException jobDependencyValidationException)
			{
				return BadRequest(jobDependencyValidationException.InnerException);
			}
			catch (JobDependencyException jobDependencyException)
			{
				return InternalServerError(jobDependencyException.InnerException);
			}
			catch (JobServiceException jobServiceException)
			{
				return InternalServerError(jobServiceException.InnerException);
			}
		}
	}
}
