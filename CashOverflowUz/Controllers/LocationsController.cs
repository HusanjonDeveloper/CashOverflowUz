//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System.Threading.Tasks;
using CashOverflowUz.Models.Locations;
using CashOverflowUz.Models.Locations.Exceptions;
using CashOverflowUz.Services.Foundetions.Locations;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace CashOverflowUz.Controllers
{
    [ApiController]
    [Route("api/Controller")]
    public class LocationsController:RESTFulController
    {
        private readonly ILocationService locationService;

        public LocationsController(ILocationService locationService)=>
            this.locationService = locationService;

        [HttpPost]

        public async ValueTask<ActionResult<Location>> PostLocationAsync(Location location)
        {
            try
            {
                Location  addedLocation = await this.locationService.AddLocationAsyncs(location);
                return Created(addedLocation);
            }
            catch(LocationValidationException locationValidationException)
            {
                return BadRequest(locationValidationException.Message);
            }
            catch()
        }
    }
}
