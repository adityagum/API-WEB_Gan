namespace Web_API.ViewModels.Accounts
{
    public class ChangePasswordVM
    {
        // Kelompok 6
        public string Email { get; set; }
        public int OTP { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
