using Coworkspace.Api.Models;

namespace Coworkspace.Api.Repositories
{
    public interface ISpaceRepository
    {
        Task<IEnumerable<Space>> GetAvailableSpaces();
        Task<Space?> GetSpaceById(int id);
        Task AddSpace(Space space);
        Task<IEnumerable<Space>> GetAvailableSpacesByIds(IEnumerable<int> reservedSpaceIds);
    }
}