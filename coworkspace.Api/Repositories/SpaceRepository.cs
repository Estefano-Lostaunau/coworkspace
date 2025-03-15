using Coworkspace.Api.Models;
using Coworkspace.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Coworkspace.Api.Repositories
{
    public class SpaceRepository : ISpaceRepository
    {
        private readonly ApiDbContext _context;

        public SpaceRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Space>> GetAvailableSpaces()
        {
            return await _context.Spaces.Where(s => s.IsAvailable).ToListAsync();
        }

        public async Task<Space?> GetSpaceById(int id)
        {
            return await _context.Spaces.FindAsync(id);
        }

        public async Task AddSpace(Space space)
        {
            await _context.Spaces.AddAsync(space);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Space>> GetAvailableSpacesByIds(IEnumerable<int> reservedSpaceIds)
        {
            return await _context.Spaces
                .Where(s => s.IsAvailable && !reservedSpaceIds.Contains(s.Id))
                .ToListAsync();
        }
    }
}