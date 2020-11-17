using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Controllers
{
    public class CacheController : ControllerBase
    {

        private readonly ILookupOnCallDevelopers _onCallLookup;

        public CacheController(ILookupOnCallDevelopers onCallLookup)
        {
            _onCallLookup = onCallLookup;
        }

        [HttpGet("/oncall")]
        public async Task<ActionResult> GetOnCallDeveloper()
        {
            string developerEmail = await _onCallLookup.GetOnCallDeveloperAsync();

            return Ok(new {
                email = developerEmail
            });
        }


        [HttpGet("/cache/time")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 15)]
        public ActionResult GetTheTime()
        {
            return Ok(new
            {
                CreatedAt = DateTime.Now.ToLongTimeString()
            });
        }
    }
}
