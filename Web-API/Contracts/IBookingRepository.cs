using Web_API.Models;

namespace Web_API.Contracts
{
    public interface IBookingRepository
    {
        Booking Create(Booking booking);
        bool Update(Booking booking);
        bool Delete(Guid guid);
        IEnumerable<Booking> GetAll();
        Booking? GetByGuid(Guid guid);
    }
}
