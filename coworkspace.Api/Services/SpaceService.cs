using Coworkspace.Api.Models;
using Coworkspace.Api.Repositories;
using Coworkspace.Api.DTOs;

namespace Coworkspace.Api.Services
{
    public class SpaceService
    {
        private readonly ISpaceRepository _spaceRepository;

        public SpaceService(ISpaceRepository spaceRepository)
        {
            _spaceRepository = spaceRepository;
        }

        public async Task<IEnumerable<Space>> GetAvailableSpaces()
        {
            return await _spaceRepository.GetAvailableSpaces();
        }

        public async Task<Space?> GetSpaceById(int id)
        {
            return await _spaceRepository.GetSpaceById(id);
        }

        public async Task<Space> CreateSpace(SpaceDTO spaceDto)
        {
            var space = new Space
            {
                Name = spaceDto.Name,
                Description = spaceDto.Description,
                IsAvailable = spaceDto.IsAvailable
            };

            await _spaceRepository.AddSpace(space);
            return space;
        }
    }
}