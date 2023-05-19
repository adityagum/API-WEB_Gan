using System.ComponentModel.DataAnnotations.Schema;

namespace Web_API.Models
{
    [Table("tb_m_account_roles")]
    public class AccountRole : BaseEntity
    {
        [Column("account_guid")]
        public Guid AccountGuid { get; set; }
        [Column("role_guid")]
        public Guid RoleGuid { get; set; }

        public Account? account { get; set; }
        public Role? role { get; set; }
    }
}
