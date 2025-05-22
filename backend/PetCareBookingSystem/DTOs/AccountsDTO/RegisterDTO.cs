using System.ComponentModel.DataAnnotations;

namespace PetCareBookingSystem.DTOs.AccountsDTO
{
    public class RegisterDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters long")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        [RegularExpression("^(010|011|012|015)\\d{8}$", ErrorMessage ="Phone Number must be 11 digit")]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[\\W_]).{8,}$", 
            ErrorMessage = "Password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
