using System.ComponentModel.DataAnnotations;

namespace PetCareBookingSystem.DTOs.PetServicesDTO
{
    public class UpdatePetServiceDTO : CreatePetServiceDTO
    {
        public int Id { get; set; }
    }
} 