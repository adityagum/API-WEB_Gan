using System.ComponentModel.DataAnnotations.Schema;
using Web_API.Utility;

namespace Web_API.Models
{
    [Table("tb_tr_bookings")]
    public class Booking : BaseEntity
    {
        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("status")]
        public StatusLevel Status { get; set; }

        [Column("remarks")]
        public string Remarks { get; set; }

        [Column("room_guid")]
        public Guid RoomGuid { get; set; }

        [Column("employee_guid")]
        public Guid EmployeeGuid { get; set; }

        public Employee Employee { get; set; }
        public Room Room { get; set; }
    }
}
