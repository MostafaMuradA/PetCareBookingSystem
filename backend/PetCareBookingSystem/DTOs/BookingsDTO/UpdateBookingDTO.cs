using System.ComponentModel.DataAnnotations;

namespace PetCareBookingSystem.DTOs.BookingsDTO
{
    public class UpdateBookingDTO
    {
        [Required]
        public string Status { get; set; }
    }
} 