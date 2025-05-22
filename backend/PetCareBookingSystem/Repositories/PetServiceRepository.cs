using PetCareBookingSystem.Models;

namespace PetCareBookingSystem.Repositories
{
    public class PetServiceRepository : GenericRepository<PetService>, IPetServiceRepository
    {
        public PetServiceRepository(PetCareDBContext context) : base(context)
        {
        }
    }
}
