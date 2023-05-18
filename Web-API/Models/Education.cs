using System.ComponentModel.DataAnnotations.Schema;

namespace Web_API.Models
{
    [Table("tb_m_educations")]
    public class Education : BaseEntity
    {
        [Column("major", TypeName = "nvarchar(100)")]
        public string Major { get; set; }

        [Column("degree", TypeName = "nvarchar(10)")]
        public string Degree { get; set; }

        [Column("gpa")]
        public float Gpa { get; set; }

        [Column("university_guid")]
        public Guid UniversityGuid { get; set; }

        //Cardinality
        public University university { get; set; }
        public Employee employee { get; set; }
    }
}
