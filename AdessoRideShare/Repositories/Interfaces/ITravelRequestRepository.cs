using AdessoRideShare.Models;

namespace AdessoRideShare.Repositories.Interfaces
{
    public interface ITravelRequestRepository
    {
        Task AddAsync(TravelRequest request);
        Task<int> GetApprovedRequestCountAsync(Guid travelPlanId);
        Task SaveChangesAsync();
        Task<List<TravelRequest>> GetRequestsByPlanIdAsync(Guid planId);
        Task<TravelRequest?> GetByIdAsync(Guid requestId);
        Task RemoveAsync(TravelRequest request);
    }
}
