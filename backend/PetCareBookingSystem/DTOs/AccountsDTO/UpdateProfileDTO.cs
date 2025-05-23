using System.ComponentModel.DataAnnotations;

namespace PetCareBookingSystem.DTOs.AccountsDTO
{
    public class UpdateProfileDTO
    {
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters long")]
        public string? UserName { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        [RegularExpression("^(010|011|012|015)\\d{8}$", ErrorMessage = "Phone Number must be 11 digit")]
        public string? PhoneNumber { get; set; }

        public string? FullName { get; set; }
    }

} 