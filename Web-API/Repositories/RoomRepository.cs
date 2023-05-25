using Web_API.Contexts;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.ViewModels.Rooms;

namespace Web_API.Repositories;

public class RoomRepository : GenericRepository<Room>, IRoomRepository
{
    /*private readonly IBookingRepository _bookingRepository;
    private readonly IEmployeeRepository _employeeRepository;*/
  /*  public RoomRepository(BookingManagementDbContext context,
        IBookingRepository bookingRepository,
        IEmployeeRepository employeeRepository) : base(context)
    {
        _bookingRepository = bookingRepository;
        _employeeRepository = employeeRepository;
    }*/

    public RoomRepository(BookingManagementDbContext context) : base(context)
    {
    }

    // Kelompok 1
    public IEnumerable<MasterRoomVM> GetByDate(DateTime dateTime)
    {
        var rooms = GetAll();
        var bookings = _context.Bookings.ToList();
        var employees = _context.Employees.ToList();

        var usedRooms = new List<MasterRoomVM>();

        foreach (var room in rooms)
        {
            var booking = bookings.FirstOrDefault(b => b.RoomGuid == room?.Guid && b.StartDate <= dateTime && b.EndDate >= dateTime);
            if (booking != null)
            {
                var employee = employees.FirstOrDefault(e => e.Guid == booking.EmployeeGuid);

                var result = new MasterRoomVM
                {
                    BookedBy = employee.FirstName + " " + employee?.LastName,
                    Status = booking.Status.ToString(),
                    RoomName = room.Name,
                    Floor = room.Floor,
                    Capacity = room.Capacity,
                    StartDate = booking.StartDate,
                    EndDate = booking.EndDate,

                };

                usedRooms.Add(result);
            }
        }

        return usedRooms;
    }

    public IEnumerable<RoomUsedVM> GetCurrentlyUsedRooms()
    {
        var rooms = GetAll();
        var bookings = _context.Bookings.ToList();
        var employees = _context.Employees.ToList();

        var usedRooms = new List<RoomUsedVM>();

        var currentTime = DateTime.Now;

        foreach (var room in rooms)
        {
            var booking = bookings.FirstOrDefault(b => b.RoomGuid == room?.Guid && b.StartDate <= currentTime && b.EndDate >= currentTime);
            if (booking != null)
            {
                var employee = employees.FirstOrDefault(e => e.Guid == booking.EmployeeGuid);

                var result = new RoomUsedVM
                {
                    RoomName = room.Name,
                    Status = booking.Status.ToString(),
                    Floor = room.Floor,
                    BookedBy = employee.FirstName + " " + employee?.LastName,
                };

                usedRooms.Add(result);
            }
        }
        return usedRooms;
    }

    // End Kelompok 1
}
