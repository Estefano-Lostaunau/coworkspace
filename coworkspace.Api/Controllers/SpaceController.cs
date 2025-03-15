using Microsoft.AspNetCore.Mvc;
using Coworkspace.Api.Services;
using Coworkspace.Api.Models;
using Coworkspace.Api.DTOs;

namespace Coworkspace.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpaceController : ControllerBase
    {
        private readonly SpaceService _spaceService;

        public SpaceController(SpaceService spaceService)
        {
            _spaceService = spaceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Space>>> GetSpaces()
        {
            var spaces = await _spaceService.GetAvailableSpaces();
            return Ok(spaces);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Space>> GetSpace(int id)
        {
            var space = await _spaceService.GetSpaceById(id);
            if (space == null)
                return NotFound();

            return Ok(space);
        }

        [HttpPost]
        public async Task<ActionResult<Space>> CreateSpace(SpaceDTO spaceDto)
        {
            var space = await _spaceService.CreateSpace(spaceDto);
            return CreatedAtAction(nameof(GetSpace), new { id = space.Id }, space);
        }
    }
}