using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public class University
    {
        public Guid guid { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
