using System.ComponentModel.DataAnnotations;

namespace PetCareBookingSystem.DTOs.BookingsDTO
{
    public class CreateBookingDTO
    {
        [Required]
        public int PetServiceId { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }
    }
} 