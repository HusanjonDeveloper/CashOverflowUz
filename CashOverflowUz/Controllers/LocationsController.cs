//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using CashOverflowUz.Models.Locations.Exceptions;
using CashOverflowUz.Models.Locations;
using CashOverflowUz.Services.Foundetions.Locations;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace CashOverflowUz.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class LocationsController : RESTFulController
	{
		private readonly ILocationService locationService;

		public LocationsController(ILocationService locationService) =>
			this.locationService = locationService;

		[HttpPost]
		public async ValueTask<ActionResult<Location>> PostLocationAsync(Location location)
		{
			try
			{
				Location addedLocation = await this.locationService.AddLocationAsync(location);

				return Created(addedLocation);
			}
			catch (LocationValidationException locationValidationException)
			{
				return BadRequest(locationValidationException.InnerException);
			}
			catch (LocationDependencyValidationException locationDependencyValidationException)
				when (locationDependencyValidationException.InnerException is AlreadyExistsLocationException)
			{
				return Conflict(locationDependencyValidationException.InnerException);
			}
			catch (LocationDependencyException locationDependencyException)
			{
				return InternalServerError(locationDependencyException.InnerException);
			}
			catch (LocationServiceException locationServiceException)
			{
				return InternalServerError(locationServiceException.InnerException);
			}
		}

		[HttpGet]
		public ActionResult<IQueryable<Location>> GetAllLocations()
		{
			try
			{
				IQueryable<Location> allLocations = this.locationService.RetrieveAllLocations();

				return Ok(allLocations);
			}
			catch (LocationDependencyException locationDependencyException)
			{
				return InternalServerError(locationDependencyException.InnerException);
			}
			catch (LocationServiceException locationServiceException)
			{
				return InternalServerError(locationServiceException.InnerException);
			}
		}

		[HttpGet("{locationId}")]
		public async ValueTask<ActionResult<Location>> GetLocationByIdAsync(Guid locationId)
		{
			try
			{
				return await this.locationService.RetrieveLocationByIdAsync(locationId);
			}
			catch (LocationDependencyException locationDependencyException)
			{
				return InternalServerError(locationDependencyException);
			}
			catch (LocationValidationException locationValidationException)
				when (locationValidationException.InnerException is InvalidLocationException)
			{
				return BadRequest(locationValidationException.InnerException);
			}
			catch (LocationValidationException locationValidationException)
				 when (locationValidationException.InnerException is NotFoundLocationException)
			{
				return NotFound(locationValidationException.InnerException);
			}
			catch (LocationServiceException locationServiceException)
			{
				return InternalServerError(locationServiceException);
			}
		}

		[HttpPut]
		public async ValueTask<ActionResult<Location>> PutLocationAsync(Location location)
		{
			try
			{
				Location modifiedLocation =
					await this.locationService.ModifyLocationAsync(location);

				return Ok(modifiedLocation);
			}
			catch (LocationValidationException locationValidationException)
				when (locationValidationException.InnerException is NotFoundLocationException)
			{
				return NotFound(locationValidationException.InnerException);
			}
			catch (LocationValidationException locationValidationException)
			{
				return BadRequest(locationValidationException.InnerException);
			}
			catch (LocationDependencyValidationException locationDependencyValidationException)
			{
				return BadRequest(locationDependencyValidationException.InnerException);
			}
			catch (LocationDependencyException locationDependencyException)
			{
				return InternalServerError(locationDependencyException.InnerException);
			}
			catch (LocationServiceException locationServiceException)
			{
				return InternalServerError(locationServiceException.InnerException);
			}
		}

		[HttpDelete("{locationId}")]
		public async ValueTask<ActionResult<Location>> DeleteLocationByIdAsync(Guid locationId)
		{
			try
			{
				Location deletedLocation = await this.locationService.RemoveLocationByIdAsync(locationId);

				return Ok(deletedLocation);
			}
			catch (LocationValidationException locationValidationException)
				when (locationValidationException.InnerException is NotFoundLocationException)
			{
				return NotFound(locationValidationException.InnerException);
			}
			catch (LocationValidationException locationValidationException)
			{
				return BadRequest(locationValidationException.InnerException);
			}
			catch (LocationDependencyValidationException locationDependencyValidationException)
				when (locationDependencyValidationException.InnerException is LockedLocationException)
			{
				return Locked(locationDependencyValidationException.InnerException);
			}
			catch (LocationDependencyValidationException locationDependencyValidationException)
			{
				return BadRequest(locationDependencyValidationException.InnerException);
			}
			catch (LocationDependencyException locationDependencyException)
			{
				return InternalServerError(locationDependencyException.InnerException);
			}
			catch (LocationServiceException locationServiceException)
			{
				return InternalServerError(locationServiceException.InnerException);
			}
		}
	}
}
