using System.ComponentModel.DataAnnotations.Schema;

namespace Web_API.Models
{
    [Table("tb_m_rooms")]
    public class Room : BaseEntity
    {
        [Column("name", TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column("floor")]
        public int Floor { get; set; }

        [Column("capacity")]
        public int Capacity { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
