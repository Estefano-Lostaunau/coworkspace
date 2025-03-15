using Coworkspace.Api.Models;

namespace Coworkspace.Api.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetUserReservations(int userId);
        Task AddReservation(Reservation reservation);
        Task<Reservation?> GetReservationById(int id);
        Task RemoveReservation(Reservation reservation);
        Task<Reservation?> GetReservationByDateAndUser(DateTime date, int userId);
        Task<IEnumerable<Reservation>> GetReservationsByDate(int userId, DateTime date);
    }
}