using AdessoRideShare.Models;

namespace AdessoRideShare.Repositories.Interfaces
{
    public interface ICityRepository
    {
        Task<List<City>> GetAllAsync();
        Task<City?> GetByIdAsync(int id);
    }
}
