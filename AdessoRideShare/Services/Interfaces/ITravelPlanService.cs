using AdessoRideShare.DTOs;

namespace AdessoRideShare.Services.Interfaces
{
    public interface ITravelPlanService
    {
        Task<List<TravelPlanDto>> GetPublishedPlansAsync(int? fromCityId, int? toCityId);
        Task<Guid> CreateTravelPlanAsync(CreateTravelPlanDto dto, Guid userId);
        Task PublishTravelPlanAsync(Guid planId, bool isPublished);
        Task<List<TravelPlanDto>> GetPlansPassingThroughAsync(int fromCityId, int toCityId);
    }
}
