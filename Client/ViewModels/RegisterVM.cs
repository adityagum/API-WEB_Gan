using System.ComponentModel.DataAnnotations;
using Web_API.Utility;

namespace Client.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        public string? LastName { get; set; }

        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }

        public DateTime HiringDate { get; set; }

        [EmailAddress]
        /*[EmailPhoneValidation(nameof(Email))]*/
        public string Email { get; set; }

        [Phone]
        /*[EmailPhoneValidation(nameof(PhoneNumber))]*/
        public string PhoneNumber { get; set; }

        public string Major { get; set; }

        public string Degree { get; set; }

        [Range(0, 4, ErrorMessage = "Must fill between 0-4")]
        public float GPA { get; set; }

        //public Guid UniversityGuid { get; set; }

        public string UniversityCode { get; set; }

        public string UniversityName { get; set; }

        [PasswordValidation(ErrorMessage = "Password must contain at least 1 Uppercase, 1 Lowercase, 1 Number, 1 Symbol, and a minimum length of 6 words")]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
