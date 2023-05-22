using System.ComponentModel.DataAnnotations.Schema;

/*
 * Jika menggunakan pattern repository, model hanya berisi properti atau singkatnya replikasi dari tabel didatabase,
 * dan karena kita menggunkan ORM, didalam model terdapat cardinalitas juga
*/

namespace Web_API.Models
{
    [Table("tb_m_accounts")]
    public class Account : BaseEntity
    {
        [Column("password")]
        public string Password { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("otp")]
        public int OTP { get; set; }

        [Column("is_used")]
        public bool IsUsed { get; set; }

        [Column("expired_time")]
        public DateTime ExpiredTime { get; set; }

        //Cardinality
        public Employee? Employee { get; set; }
        public ICollection<AccountRole>? AccountRoles { get; set; }
    }
}
