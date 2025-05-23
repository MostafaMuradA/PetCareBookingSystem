using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareBookingSystem.Models;
using PetCareBookingSystem.Repositories;
using PetCareBookingSystem.DTOs.BookingsDTO;
using PetCareBookingSystem.Constants;
using System.Security.Claims;

namespace PetCareBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository repository;
        private readonly IPetServiceRepository petServiceRepository;
        private readonly UserManager<User> userManager;

        public BookingsController(
            IBookingRepository repository,
            IPetServiceRepository petServiceRepository,
            UserManager<User> userManager)
        {
            this.repository = repository;
            this.petServiceRepository = petServiceRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetAll()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var isAdmin = User.IsInRole(Roles.Admin);

                var bookings = await repository.GetAll();
                if (!isAdmin)
                {
                    bookings = bookings.Where(b => b.UserId == userId).ToList();
                }

                var bookingDTOs = bookings.Select(b => new BookingDTO
                {
                    Id = b.Id,
                    UserId = b.UserId,
                    UserName = b.User.UserName,
                    PetServiceId = b.PetServiceId,
                    ServiceName = b.PetService.ServiceName,
                    BookingDate = b.BookingDate,
                    Status = b.Status,
                    Price = b.PetService.Price,
                    Duration = b.PetService.Duration
                });

                return Ok(bookingDTOs);
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<BookingDTO>> GetById(int id)
        {
            var booking = await repository.GetById(id);
            if (booking == null) return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole(Roles.Admin);

            if (!isAdmin && booking.UserId != userId)
                return Forbid();

            var bookingDTO = new BookingDTO
            {
                Id = booking.Id,
                UserId = booking.UserId,
                UserName = booking.User.UserName,
                PetServiceId = booking.PetServiceId,
                ServiceName = booking.PetService.ServiceName,
                BookingDate = booking.BookingDate,
                Status = booking.Status,
                Price = booking.PetService.Price,
                Duration = booking.PetService.Duration
            };

            return Ok(bookingDTO);
        }

        [HttpPost]  
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<BookingDTO>> Create(CreateBookingDTO createDTO)
        {
            try
            {
                // Validate booking date
                if (createDTO.BookingDate <= DateTime.Now)
                {
                    return BadRequest(new { message = "Booking date must be in the future." });
                }

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var service = await petServiceRepository.GetById(createDTO.PetServiceId);
                if (service == null) return BadRequest(new { message = "Invalid service ID" });

                // Check for overlapping bookings
                var existingBookings = await repository.GetAll();
                var overlappingBooking = existingBookings.FirstOrDefault(b =>
                    b.PetServiceId == createDTO.PetServiceId &&
                    b.Status != "Cancelled" &&
                    b.BookingDate <= createDTO.BookingDate.AddMinutes(service.Duration) &&
                    createDTO.BookingDate <= b.BookingDate.AddMinutes(b.PetService.Duration));

                if (overlappingBooking != null)
                {
                    return BadRequest(new { message = "This time slot is already booked." });
                }

                var booking = new Booking
                {
                    UserId = userId,
                    PetServiceId = createDTO.PetServiceId,
                    BookingDate = createDTO.BookingDate,
                    Status = "Pending"
                };

                await repository.Add(booking);
                await repository.Save();

                var user = await userManager.FindByIdAsync(userId);

                var bookingDTO = new BookingDTO
                {
                    Id = booking.Id,
                    UserId = booking.UserId,
                    UserName = user.UserName,
                    PetServiceId = booking.PetServiceId,
                    ServiceName = service.ServiceName,
                    BookingDate = booking.BookingDate,
                    Status = booking.Status,
                    Price = service.Price,
                    Duration = service.Duration
                };

                return CreatedAtAction(nameof(GetById), new { id = booking.Id }, bookingDTO);
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateBookingDTO updateDTO)
        {
            var booking = await repository.GetById(id);
            if (booking == null) return NotFound();

            booking.Status = updateDTO.Status;

            await repository.Update(booking);
            await repository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await repository.GetById(id);
            if (booking == null) return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole(Roles.Admin);

            if (!isAdmin && booking.UserId != userId)
                return Forbid();

            if (booking.Status != "Pending")
                return BadRequest("Cannot delete a booking that is not in pending status");

            await repository.Delete(id);
            await repository.Save();

            return NoContent();
        }
    }
} 