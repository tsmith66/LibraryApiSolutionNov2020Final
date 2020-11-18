using LibraryAPI.Models.BookReservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Services
{
    public class RabbitBookReservationProcessor : IProcessBookReservations
    {
        public Task LogOrder(GetReservationResponse request)
        {
            throw new NotImplementedException();
        }
    }
}
