﻿using Web_API.Models;
using Web_API.ViewModels.Bookings;

namespace Web_API.Contracts;

public interface IBookingRepository : IGenericRepository<Booking>
{
    IEnumerable<BookingDurationVM> GetBookingDuration();

    // Kelompok 4
    IEnumerable<BookingDetailVM> GetAllBookingDetail();
    BookingDetailVM GetBookingDetailByGuid(Guid guid);
}
