using Microsoft.AspNetCore.Mvc;
using Coworkspace.Api.Services;
using Coworkspace.Api.DTOs;
using Coworkspace.Api.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Coworkspace.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var reservations = await _reservationService.GetUserReservations(userId);
            var response = reservations.Select(r => new
            {
                r.Id,
                User = new UserResponseDTO
                {
                    Id = r.User.Id,
                    Name = r.User.Name,
                    Email = r.User.Email
                },
                r.Space,
                r.ReservationDate,
                r.StartTime,
                r.EndTime
            }).ToList();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> CreateReservation(ReservationDTO reservationDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var reservation = await _reservationService.CreateReservation(reservationDto, userId);
            var response = new
            {
                reservation.Id,
                User = new UserResponseDTO
                {
                    Id = reservation.User.Id,
                    Name = reservation.User.Name,
                    Email = reservation.User.Email
                },
                reservation.Space,
                reservation.ReservationDate,
                reservation.StartTime,
                reservation.EndTime
            };
            return CreatedAtAction(nameof(GetReservations), new { id = reservation.Id }, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelReservation(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var result = await _reservationService.CancelReservation(id, userId);
            if (!result)
                return BadRequest("No se puede cancelar la reserva. Asegúrese de que falte más de una hora para el inicio de la reserva.");

            return Ok("Eliminación con éxito");
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetAvailableReservations([FromQuery] DateTime date)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var reservations = await _reservationService.GetReservationsByDate(userId, date);
            var response = reservations.Select(r => new
            {
                r.Id,
                User = new UserResponseDTO
                {
                    Id = r.User.Id,
                    Name = r.User.Name,
                    Email = r.User.Email
                },
                r.Space,
                r.ReservationDate,
                r.StartTime,
                r.EndTime
            }).ToList();
            return Ok(response);
        }
    }
}