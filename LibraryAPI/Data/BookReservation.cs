using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Data
{
    public enum BookReservationStatus { Pending, Approved, Denied}
    public class BookReservation
    {
        public int Id { get; set; }
        public string For { get; set; }
        public string BooksReserved { get; set;  } // "1,2,3,4"
        public BookReservationStatus Status { get; set; }
    }
}
