using System.ComponentModel.DataAnnotations.Schema;
using Web_API.ViewModels.Educations;

namespace Web_API.ViewModels.Accounts;

public class AccountVM
{
    public Guid? Guid { get; set; }
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
    public int OTP { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredTime { get; set; }

    /*public IEnumerable<AccountAccountRoleVM> Accounts { get; set; }*/
}

