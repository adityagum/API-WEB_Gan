using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_API.ViewModels.Rooms
{
    public class RoomVM
    {
        public string Name { get; set; }

        public int Floor { get; set; }

        public int Capacity { get; set; }
    }
}
