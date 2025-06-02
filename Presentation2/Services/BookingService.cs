using Presentation2.Data.Entities;
using Presentation2.Interface;
using Presentation2.Models;

namespace Presentation2.Services;

public class BookingService(IBookingRepository bookingRepository, BookingVerification email) : IBookingService
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly BookingVerification _email = email;

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

        if (result.Success)
        {
            // Skicka bekräftelsemail
            var subject = "Bokningsbekräftelse - Ventixe";
            var message = $@"
            Hej {request.FirstName} {request.LastName},

            Tack för din bokning!

            Antal biljetter: {request.TicketQuantity}
            Event ID: {request.EventId}

            Vi ses snart!
            Hälsningar,
            Ventixe-teamet
             ";

            await _email.SendEmailAsync(request.Email, subject, message);

            return new BookingResult { Success = true };
        }

        return new BookingResult { Success = false, Error = result.Error };
    }
}
