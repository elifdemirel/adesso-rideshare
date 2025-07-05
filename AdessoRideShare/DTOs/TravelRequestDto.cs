namespace AdessoRideShare.DTOs
{
    public class CreateTravelRequestDto
    {
        public Guid TravelPlanId { get; set; }
        public string Message { get; set; }
    }

    public class TravelRequestDto
    {
        public Guid Id { get; set; }
        public Guid TravelPlanId { get; set; }
        public Guid UserId { get; set; }
        public DateTime RequestDate { get; set; }
        public bool IsApproved { get; set; }
    }

    public class TravelRequestListItemDto
    {
        public Guid RequestId { get; set; }
        public Guid UserId { get; set; }
        public string? Message { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
