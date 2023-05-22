using System.ComponentModel.DataAnnotations.Schema;

namespace Web_API.ViewModels.AccountRoles
{
    public class AccountRolesVM
    {
        public Guid? Guid { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }
    }
}
