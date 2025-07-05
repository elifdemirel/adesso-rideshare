using System;
namespace AdessoRideShare.Models
{
    public class TravelRequest
    {
        public Guid Id { get; set; }
        public Guid TravelPlanId { get; set; }
        public Guid UserId { get; set; }
        public DateTime RequestDate { get; set; }
        public bool IsApproved { get; set; }
        public string? Message { get; set; }

        public TravelPlan TravelPlan { get; set; }
        public User User { get; set; }
    }
}

