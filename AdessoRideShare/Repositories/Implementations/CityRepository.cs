using AdessoRideShare.Data;
using AdessoRideShare.Models;
using AdessoRideShare.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdessoRideShare.Repositories.Implementations
{
    public class CityRepository : ICityRepository
    {
        private readonly RideShareDbContext _context;

        public CityRepository(RideShareDbContext context)
        {
            _context = context;
        }

        public async Task<List<City>> GetAllAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<City?> GetByIdAsync(int id)
        {
            return await _context.Cities.FindAsync(id);
        }
    }
}
