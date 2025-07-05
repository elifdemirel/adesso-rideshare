using System;
namespace AdessoRideShare.Models
{
    public class TravelPlan
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int FromCityId { get; set; }
        public int ToCityId { get; set; }
        public DateTime TravelDate { get; set; }
        public string Description { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public bool IsPublished { get; set; }

        public User User { get; set; }
        public City FromCity { get; set; }
        public City ToCity { get; set; }
    }
}

