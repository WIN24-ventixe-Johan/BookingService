using Presentation2.Data.Entities;
using Presentation2.Interface;
using Presentation2.Models;

namespace Presentation2.Services;

public class BookingService(IBookingRepository bookingRepository) : IBookingService
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;

    public async Task<BookingResult> CreateBookingAsync(CreateBookingRequest request)
    {
        var bookingEntity = new BookingEntity
        {
            EventId = request.EventId,
            BookingDate = DateTime.Now,
            TicketQuantity = request.TicketQuantity,
            BookingOwner = new BookingOwnerEntity
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Address = new BookingAddressEntity
                {
                    StreetName = request.StreetName,
                    PostalCode = request.PostalCode,
                    City = request.City,
                }
            }
        };

        var result = await _bookingRepository.AddAsync(bookingEntity);
        return result.Success
            ? new BookingResult { Success = true, }
            : new BookingResult { Success = false, Error = result.Error };

    }
}
