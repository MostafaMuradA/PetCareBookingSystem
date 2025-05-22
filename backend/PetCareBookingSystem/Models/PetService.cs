namespace PetCareBookingSystem.Models
{
    public class PetService
    {
        public int Id { get; set; }
        public string ServiceName { get; set; } = null!;
        public string ServiceImg { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Duration { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
