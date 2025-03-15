using Coworkspace.Api.Models;
using Coworkspace.Api.DTOs;
using Coworkspace.Api.Repositories;

namespace Coworkspace.Api.Services
{
    public class ReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ISpaceRepository _spaceRepository;
        private readonly IUserRepository _userRepository;

        public ReservationService(IReservationRepository reservationRepository, ISpaceRepository spaceRepository, IUserRepository userRepository)
        {
            _reservationRepository = reservationRepository;
            _spaceRepository = spaceRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Reservation>> GetUserReservations(int userId)
        {
            return await _reservationRepository.GetUserReservations(userId);
        }

        public async Task<Reservation> CreateReservation(ReservationDTO dto, int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            if (dto.ReservationDate.Date < DateTime.UtcNow.Date)
            {
                throw new Exception("No se puede hacer una reserva en el pasado");
            }

            var existingReservation = await _reservationRepository.GetReservationByDateAndUser(dto.ReservationDate, userId);
            if (existingReservation != null)
            {
                throw new Exception("El usuario ya tiene una reserva para esta fecha");
            }

            var reservation = new Reservation
            {
                UserId = userId,
                SpaceId = dto.SpaceId,
                ReservationDate = dto.ReservationDate,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            await _reservationRepository.AddReservation(reservation);
            reservation.Space = await _spaceRepository.GetSpaceById(reservation.SpaceId) ?? throw new Exception("Espacio no encontrado");
            reservation.User = user;
            return reservation;
        }

        public async Task<bool> CancelReservation(int id, int userId)
        {
            var reservation = await _reservationRepository.GetReservationById(id);
            if (reservation == null || reservation.UserId != userId)
                return false;

            if ((reservation.StartTime - DateTime.UtcNow).TotalHours < 1)
                return false;

            await _reservationRepository.RemoveReservation(reservation);
            return true;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByDate(int userId, DateTime date)
        {
            return await _reservationRepository.GetReservationsByDate(userId, date);
        }
    }
}