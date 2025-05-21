using Microsoft.AspNetCore.Identity;

namespace PetCareBookingSystem.Models
{
    public class User : IdentityUser
    {
        
        public string FullName { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
