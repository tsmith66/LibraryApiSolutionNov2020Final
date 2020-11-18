using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Models.BookReservations
{
    public class PostReservationRequest : IValidatableObject
    {
        [Required]
        public string For { get; set; }
        [Required]
        public int[] Books { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Books.Length == 0)
            {
                yield return new ValidationResult("You should ask for some books, bonehead.", new string[] { "Books" });
            }
            if (For.ToLower() == "henry" && Books.Contains(1))
            {
                yield return new ValidationResult("Henry has read 'Diary of a Wimpy Kid' too many times already", new string[] { "For", "Books" });
            }
        }
    }

}
