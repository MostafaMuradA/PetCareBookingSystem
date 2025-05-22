using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareBookingSystem.Models;
using PetCareBookingSystem.Repositories;
using PetCareBookingSystem.DTOs;
using PetCareBookingSystem.Constants;

namespace PetCareBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetServicesController : ControllerBase
    {
        private readonly IPetServiceRepository repository;

        public PetServicesController(IPetServiceRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult<IEnumerable<PetServiceDTO>>> GetAll()
        {
            var services = await repository.GetAll();
            var serviceDTOs = services.Select(s => new PetServiceDTO
            {
                Id = s.Id,
                Name = s.ServiceName,
                Description = s.Description,
                Price = s.Price,
                Duration = s.Duration
            });
            return Ok(serviceDTOs);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult<PetServiceDTO>> GetById(int id)
        {
            var service = await repository.GetById(id);
            if (service == null) return NotFound();

            var serviceDTO = new PetServiceDTO
            {
                Id = service.Id,
                Name = service.ServiceName,
                Description = service.Description,
                Price = service.Price,
                Duration = service.Duration
            };
            return Ok(serviceDTO);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PetServiceDTO>> Add(CreatePetServiceDTO createDTO)
        {
            var service = new PetService
            {
                ServiceName = createDTO.Name,
                Description = createDTO.Description,
                Price = createDTO.Price,
                Duration = createDTO.Duration,
                ServiceImg = createDTO.ServiceImg ?? "default-service.jpg"
            };

            await repository.Add(service);
            await repository.Save();

            var serviceDTO = new PetServiceDTO
            {
                Id = service.Id,
                Name = service.ServiceName,
                Description = service.Description,
                Price = service.Price,
                Duration = service.Duration
            };

            return CreatedAtAction(nameof(GetById), new { id = service.Id }, serviceDTO);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdatePetServiceDTO updateDTO)
        {
            if (id != updateDTO.Id) return BadRequest("ID mismatch");

            var existingService = await repository.GetById(id);
            if (existingService == null) return NotFound();

            existingService.ServiceName = updateDTO.Name;
            existingService.Description = updateDTO.Description;
            existingService.Price = updateDTO.Price;
            existingService.Duration = updateDTO.Duration;
            if (!string.IsNullOrEmpty(updateDTO.ServiceImg))
            {
                existingService.ServiceImg = updateDTO.ServiceImg;
            }

            await repository.Update(existingService);
            await repository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await repository.GetById(id);
            if (existing == null) return NotFound();

            await repository.Delete(id);
            await repository.Save();

            return NoContent();
        }
    }
}
