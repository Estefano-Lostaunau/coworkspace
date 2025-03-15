using Coworkspace.Api.Models;
using Coworkspace.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Coworkspace.Api.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApiDbContext _context;

        public ReservationRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetUserReservations(int userId)
        {
            return await _context.Reservations
                .Where(r => r.UserId == userId)
                .Include(r => r.User)
                .Include(r => r.Space)
                .ToListAsync();
        }

        public async Task AddReservation(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<Reservation?> GetReservationById(int id)
        {
            return await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Space)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task RemoveReservation(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<Reservation?> GetReservationByDateAndUser(DateTime date, int userId)
        {
            return await _context.Reservations
                .Where(r => r.ReservationDate.Date == date.Date && r.UserId == userId)
                .Include(r => r.User)
                .Include(r => r.Space)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByDate(int userId, DateTime date)
        {
            return await _context.Reservations
                .Where(r => r.UserId == userId && r.ReservationDate.Date == date.Date)
                .Include(r => r.User)
                .Include(r => r.Space)
                .ToListAsync();
        }
    }
}