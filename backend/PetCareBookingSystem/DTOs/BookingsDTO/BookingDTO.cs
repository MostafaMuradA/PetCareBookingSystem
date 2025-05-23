using System.ComponentModel.DataAnnotations;

namespace PetCareBookingSystem.DTOs.BookingsDTO
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int PetServiceId { get; set; }
        public string ServiceName { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
    }
} 