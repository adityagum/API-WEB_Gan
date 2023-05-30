using Web_API.Utility;

namespace Web_API.ViewModels.Accounts
{
    public class ChangePasswordVM
    {
        // Kelompok 6
        public string Email { get; set; }
        public int OTP { get; set; }

        [PasswordValidation]
        public string NewPassword { get; set; }

        [PasswordValidation]
        public string ConfirmPassword { get; set; }
    }
}
