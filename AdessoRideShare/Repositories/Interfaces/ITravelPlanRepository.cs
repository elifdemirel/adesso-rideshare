using AdessoRideShare.Models;

namespace AdessoRideShare.Repositories.Interfaces
{
    public interface ITravelPlanRepository
    {
        Task<List<TravelPlan>> GetPublishedPlansAsync(int? fromCityId = null, int? toCityId = null);
        Task<TravelPlan?> GetByIdAsync(Guid id);
        Task<List<TravelPlan>> GetAllAsync();
        Task AddAsync(TravelPlan plan);
        Task UpdateAsync(TravelPlan plan);
        Task SaveChangesAsync();
    }
}
