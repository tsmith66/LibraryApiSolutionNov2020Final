using LibraryAPI.Models.BookReservations;
using System.Threading.Tasks;

namespace LibraryAPI
{
    public interface IProcessBookReservations
    {
        Task LogOrder(PostReservationRequest request);
    }
}