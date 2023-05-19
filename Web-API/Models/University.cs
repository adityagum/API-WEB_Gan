using System.ComponentModel.DataAnnotations.Schema;

namespace Web_API.Models
{
    [Table("tb_m_universities")]
    public class University : BaseEntity
    {
        [Column("code", TypeName = "nvarchar(50)")]
        public string Code { get; set; }

        [Column("name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        //Cardinalitas
        public ICollection<Education>? Educations { get; set; }
    }
}
