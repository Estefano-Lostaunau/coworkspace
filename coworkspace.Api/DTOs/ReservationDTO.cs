namespace Coworkspace.Api.DTOs
{
    public class ReservationDTO
    {
        public int SpaceId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}