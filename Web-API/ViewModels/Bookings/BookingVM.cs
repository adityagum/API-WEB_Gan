using System.ComponentModel.DataAnnotations.Schema;
using Web_API.Utility;

namespace Web_API.ViewModels.Bookings;

public class BookingVM
{
    public Guid? Guid { get; set; }
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public StatusLevel Status { get; set; }

    public string Remarks { get; set; }

    public Guid RoomGuid { get; set; }
    public Guid EmployeeGuid { get; set; }

}
