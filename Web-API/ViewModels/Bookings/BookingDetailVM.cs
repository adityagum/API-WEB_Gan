using Web_API.Utility;

namespace Web_API.ViewModels.Bookings
{
    // Kelompok 4
    public class BookingDetailVM
    {
        public Guid? Guid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevel Status { get; set; }
        public string Remarks { get; set; }
        public string BookedNIK { get; set; }
        public string Fullname { get; set; }
        public string RoomName { get; set; }

    }
}
