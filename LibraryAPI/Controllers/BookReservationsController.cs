using LibraryAPI.Filters;
using LibraryAPI.Models.BookReservations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Controllers
{
    public class BookReservationsController : ControllerBase
    {
        [HttpPost("bookreservations")]
        [ValidateModel]
        public async Task<ActionResult> AddReservation([FromBody] PostReservationRequest request)
        {
            return Ok(request);

        }
    }
}
