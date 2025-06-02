using Presentation2.Models;

namespace Presentation2.Services
{
    public interface IBookingService
    {
        Task<BookingResult> CreateBookingAsync(CreateBookingRequest request);
    }
}