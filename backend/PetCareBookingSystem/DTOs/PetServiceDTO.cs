using System.ComponentModel.DataAnnotations;

namespace PetCareBookingSystem.DTOs
{
    public class PetServiceDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public decimal Price { get; set; }
        
        public int Duration { get; set; }
    }

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

    public class UpdatePetServiceDTO : CreatePetServiceDTO
    {
        public int Id { get; set; }
    }
} 