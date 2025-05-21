namespace PetCareBookingSystem.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public string UserId { get; set; } = null!;
        public virtual User User { get; set; } = null!;

        public int PetServiceId { get; set; }
        public virtual PetService PetService { get; set; } = null!;

        public DateTime BookingDate { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
