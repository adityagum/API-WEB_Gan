using Microsoft.Identity.Client;
using Web_API.Contexts;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.ViewModels.Accounts;

namespace Web_API.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(BookingManagementDbContext context) : base(context) { }

        public IEnumerable<Booking> GetByEmployeeGuid(Guid employeeId)
        {
            return _context.Set<Booking>().Where(e => e.EmployeeGuid == employeeId);
        }

        public IEnumerable<Booking> GetByRoomGuid(Guid roomId)
        {
            return _context.Set<Booking>().Where(e => e.RoomGuid == roomId);
        }
    }
}
