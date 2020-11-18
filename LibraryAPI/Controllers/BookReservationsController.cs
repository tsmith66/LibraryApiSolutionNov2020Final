using LibraryAPI.Data;
using LibraryAPI.Filters;
using LibraryAPI.Models.BookReservations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Controllers
{
    public class BookReservationsController : ControllerBase
    {
        private readonly LibraryDataContext _context;
        private readonly IProcessBookReservations _orderProcessor;
        public BookReservationsController(LibraryDataContext context, IProcessBookReservations orderProcessor)
        {
            _context = context;
            _orderProcessor = orderProcessor;
        }

        [HttpPost("/bookreservations/approved")]
        [ValidateModel]
        public async Task<ActionResult> ApproveReservation([FromBody] GetReservationResponse request)
        {
            var savedReservation = await _context.Reservations
                .SingleOrDefaultAsync(b => b.Id == request.Id);

            if(savedReservation == null)
            {
                return BadRequest("Could not find that reservation");
            } else
            {
                savedReservation.Status = BookReservationStatus.Approved;
                await _context.SaveChangesAsync();
                return Ok();
            }

        }
        [HttpPost("/bookreservations/denied")]
        [ValidateModel]
        public async Task<ActionResult> DenyReservation([FromBody] GetReservationResponse request)
        {
            var savedReservation = await _context.Reservations
                .SingleOrDefaultAsync(b => b.Id == request.Id);

            if (savedReservation == null)
            {
                return BadRequest("Could not find that reservation");
            }
            else
            {
                savedReservation.Status = BookReservationStatus.Denied;
                await _context.SaveChangesAsync();
                return Ok();
            }

        }


        [HttpPost("bookreservations")]
        [ValidateModel]
        public async Task<ActionResult> AddReservation([FromBody] PostReservationRequest request)
        {

            // 1. Validate - Done.
            // 2. Map the Model to an Entity (PostReservationRequest -> BookReservation)
           // await Task.Delay(1000 * request.Books.Length);
           
           
            var book = new BookReservation
            {
                For = request.For,
                BooksReserved = String.Join(",", request.Books),
                Status = BookReservationStatus.Pending
            };
            
           

            // 3. Add that to the DataContext
            _context.Reservations.Add(book);
           
            // 4. Save it.
            await _context.SaveChangesAsync();
            // 5. Return:
            //    a) 201 Created status code "I created a new resource for you"
            //    b) A Location header with the URL of the new thingy.
            //    c) Attach a copy of whatever they would get if they did a get request to the location.
            //    d) Consider adding a cache-control header. We won't do this now.
            var response = new GetReservationResponse
            {
                Id = book.Id,
                For = book.For,
                BooksReserved = book.BooksReserved,
                Status = book.Status
            };
            // tell "someone" else to do this offline.
            await _orderProcessor.LogOrder(response);
            return CreatedAtRoute("bookreservations#get-byid", new { id = book.Id }, response);

        }

        [HttpGet("bookreservations/{id:int}", Name ="bookreservations#get-byid")]
        public async Task<ActionResult> GetReservationById(int id)
        {
            // look up the book.
            var book = await _context.Reservations.SingleOrDefaultAsync(r => r.Id == id);
            // if no book, return 404
            if (book == null)
            {
                return NotFound();
            }
            else
            {
                // if a book
                //  --map it to our model
                //  -- return it with a 200
                var response = new GetReservationResponse
                {
                    Id = book.Id,
                    For = book.For,
                    BooksReserved = book.BooksReserved,
                    Status = book.Status
                };
                return Ok(response);
            }

        }
    }
}
