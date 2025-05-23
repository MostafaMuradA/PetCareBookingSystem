using System.ComponentModel.DataAnnotations;

namespace PetCareBookingSystem.DTOs.PetServicesDTO
{
    public class CreatePetServiceDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Duration { get; set; }

        public string? ServiceImg { get; set; }
    }
} 