using Web_API.Models;
using Web_API.ViewModels.Rooms;

namespace Web_API.Contracts
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        // Kelompok 1
        IEnumerable<MasterRoomVM> GetByDate(DateTime dateTime);
        IEnumerable<RoomUsedVM> GetCurrentlyUsedRooms();
    }
}
