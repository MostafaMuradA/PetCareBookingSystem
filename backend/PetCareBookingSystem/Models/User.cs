using Microsoft.AspNetCore.Identity;

namespace PetCareBookingSystem.Models
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public string? UserImg { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
