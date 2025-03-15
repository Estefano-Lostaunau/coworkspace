namespace Coworkspace.Api.DTOs
{
    public class SpaceDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public bool IsAvailable { get; set; }
    }
}