namespace AdessoRideShare.DTOs
{
    public class TravelPlanDto
    {
        public Guid Id { get; set; }
        public int FromCityId { get; set; }
        public int ToCityId { get; set; }
        public DateTime TravelDate { get; set; }
        public string Description { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public bool IsPublished { get; set; }
    }

    public class CreateTravelPlanDto
    {
        public int FromCityId { get; set; }
        public int ToCityId { get; set; }
        public DateTime TravelDate { get; set; }
        public string Description { get; set; }
        public int TotalSeats { get; set; }
    }
}
