using Web_API.Models;

namespace Web_API.Contracts
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        /*IEnumerable<Booking> GetByRoomGuid(Guid roomId);
        IEnumerable<Booking> GetByEmployeeGuid(Guid employeeId);*/
    }
}
