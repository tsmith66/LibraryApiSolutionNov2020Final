﻿using LibraryAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Models.BookReservations
{
    public class GetReservationResponse
    {
        public int Id { get; set; }
        public string For { get; set; }
        public string BooksReserved { get; set; } // "1,2,3,4"
        public BookReservationStatus Status { get; set; }
    }
}
