using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_API.ViewModels.AccountRoles
{
    public class AccountRolesVM
    {

        public Guid? Guid { get; set; }

        /*[Required(ErrorMessage = "Account GUID is required")]*/
        public Guid AccountGuid { get; set; }

        /*[Required(ErrorMessage = "Role GUID is required")]*/
        public Guid RoleGuid { get; set; }
    }
}
