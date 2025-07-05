using AdessoRideShare.Data;
using AdessoRideShare.Models;
using AdessoRideShare.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdessoRideShare.Repositories.Implementations
{
    public class TravelPlanRepository : ITravelPlanRepository
    {
        private readonly RideShareDbContext _context;

        public TravelPlanRepository(RideShareDbContext context)
        {
            _context = context;
        }

        public async Task<List<TravelPlan>> GetPublishedPlansAsync(int? fromCityId = null, int? toCityId = null)
        {
            var query = _context.TravelPlans
                .Include(tp => tp.FromCity)
                .Include(tp => tp.ToCity)
                .Where(tp => tp.IsPublished && tp.AvailableSeats > 0);

            if (fromCityId.HasValue)
                query = query.Where(tp => tp.FromCityId == fromCityId);

            if (toCityId.HasValue)
                query = query.Where(tp => tp.ToCityId == toCityId);

            return await query.ToListAsync();
        }

        public async Task<TravelPlan?> GetByIdAsync(Guid id)
        {
            return await _context.TravelPlans.FindAsync(id);
        }

        public async Task<List<TravelPlan>> GetAllAsync()
        {
            return await _context.TravelPlans.ToListAsync();
        }

        public async Task AddAsync(TravelPlan plan)
        {
            await _context.TravelPlans.AddAsync(plan);
        }

        public async Task UpdateAsync(TravelPlan plan)
        {
            _context.TravelPlans.Update(plan);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
