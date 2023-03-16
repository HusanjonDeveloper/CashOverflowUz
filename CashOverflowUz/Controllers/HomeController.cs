//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using Microsoft.AspNetCore.Mvc;

namespace CashOverflowUz.Controllers
{
    [ApiController]
    [Route("api/Controller")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public string Get() => "Cash Flows...";
    }
}
