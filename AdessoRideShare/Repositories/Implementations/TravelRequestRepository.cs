using AdessoRideShare.Data;
using AdessoRideShare.Models;
using AdessoRideShare.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdessoRideShare.Repositories.Implementations
{
    public class TravelRequestRepository : ITravelRequestRepository
    {
        private readonly RideShareDbContext _context;

        public TravelRequestRepository(RideShareDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TravelRequest request)
        {
            await _context.TravelRequests.AddAsync(request);
        }

        public async Task<int> GetApprovedRequestCountAsync(Guid travelPlanId)
        {
            return await _context.TravelRequests
                .CountAsync(r => r.TravelPlanId == travelPlanId && r.IsApproved);
        }

        public async Task<List<TravelRequest>> GetRequestsByPlanIdAsync(Guid planId)
        {
            return await _context.TravelRequests
                .Where(r => r.TravelPlanId == planId)
                .ToListAsync();
        }

        public async Task<TravelRequest?> GetByIdAsync(Guid requestId)
        {
            return await _context.TravelRequests
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == requestId);
        }

        public Task RemoveAsync(TravelRequest request)
        {
            _context.TravelRequests.Remove(request);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
