using System.ComponentModel.DataAnnotations;

namespace PetCareBookingSystem.DTOs.PetServicesDTO
{
    public class PetServiceDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public decimal Price { get; set; }
        
        public int Duration { get; set; }

        public string? ServiceImg { get; set; }
    }
} 