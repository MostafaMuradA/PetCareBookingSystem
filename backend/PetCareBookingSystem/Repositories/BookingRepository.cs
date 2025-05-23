using Microsoft.EntityFrameworkCore;
using PetCareBookingSystem.Models;

namespace PetCareBookingSystem.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {

        public BookingRepository(PetCareDBContext context) : base(context)
        {   
        }
    }
} 