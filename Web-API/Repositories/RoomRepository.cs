using Web_API.Contexts;
using Web_API.Contracts;
using Web_API.Models;

namespace Web_API.Repositories
{
    public class RoomRepoository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepoository(BookingManagementDbContext context) : base(context) { }
    }
}
