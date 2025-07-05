using AdessoRideShare.DTOs;
using AdessoRideShare.Models;
using AdessoRideShare.Repositories.Interfaces;
using AdessoRideShare.Services.Helpers;
using AdessoRideShare.Services.Interfaces;

namespace AdessoRideShare.Services.Implementations
{
    public class TravelPlanService : ITravelPlanService
    {
        private readonly ITravelPlanRepository _repository;
        private readonly ICityRepository _cityRepository;

        public TravelPlanService(ITravelPlanRepository repository, ICityRepository cityRepository)
        {
            _repository = repository;
            _cityRepository = cityRepository;
        }

        public async Task<List<TravelPlanDto>> GetPublishedPlansAsync(int? fromCityId, int? toCityId)
        {
            var plans = await _repository.GetPublishedPlansAsync(fromCityId, toCityId);
            return plans.Select(p => new TravelPlanDto
            {
                Id = p.Id,
                FromCityId = p.FromCityId,
                ToCityId = p.ToCityId,
                TravelDate = p.TravelDate,
                Description = p.Description,
                TotalSeats = p.TotalSeats,
                AvailableSeats = p.AvailableSeats,
                IsPublished = p.IsPublished
            }).ToList();
        }

        public async Task<Guid> CreateTravelPlanAsync(CreateTravelPlanDto dto, Guid userId)
        {
            var allCities = await _cityRepository.GetAllAsync();

            var fromCityExists = allCities.Any(c => c.Id == dto.FromCityId);
            var toCityExists = allCities.Any(c => c.Id == dto.ToCityId);

            if (!fromCityExists || !toCityExists)
                throw new Exception("Geçerli kalkış veya varış şehri bulunamadı.");

            var plan = new TravelPlan
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                FromCityId = dto.FromCityId,
                ToCityId = dto.ToCityId,
                TravelDate = dto.TravelDate,
                Description = dto.Description,
                TotalSeats = dto.TotalSeats,
                AvailableSeats = dto.TotalSeats,
                IsPublished = false
            };

            await _repository.AddAsync(plan);
            await _repository.SaveChangesAsync();

            return plan.Id;
        }

        public async Task PublishTravelPlanAsync(Guid planId, bool isPublished)
        {
            var plan = await _repository.GetByIdAsync(planId);
            if (plan == null) throw new Exception("Plan not found");

            plan.IsPublished = isPublished;
            await _repository.UpdateAsync(plan);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<TravelPlanDto>> GetPlansPassingThroughAsync(int fromCityId, int toCityId)
        {
            var allCities = await _cityRepository.GetAllAsync();
            var fromCity = allCities.FirstOrDefault(c => c.Id == fromCityId);
            var toCity = allCities.FirstOrDefault(c => c.Id == toCityId);

            if (fromCity == null || toCity == null)
                throw new Exception("Şehirler bulunamadı");

            var route = PathFinder.GetRoute(fromCity, toCity, allCities);

            var allPlans = await _repository.GetAllAsync();
            var validPlans = new List<TravelPlan>();

            // Doğrudan eşleşen planları her koşulda ekle
            validPlans.AddRange(allPlans.Where(p =>
                p.IsPublished &&
                p.FromCityId == fromCityId &&
                p.ToCityId == toCityId));

            // Rota varsa geçişli planları da ekle
            if (route != null && route.Count > 1)
            {
                var cityIdsOnRoute = route.Select(c => c.Id).ToList();

                var passingPlans = allPlans.Where(p =>
                    p.IsPublished &&
                    cityIdsOnRoute.Contains(p.FromCityId) &&
                    cityIdsOnRoute.Contains(p.ToCityId) &&
                    cityIdsOnRoute.IndexOf(p.FromCityId) < cityIdsOnRoute.IndexOf(p.ToCityId) &&
                    !(p.FromCityId == fromCityId && p.ToCityId == toCityId) // zaten yukarıda eklendi
                );

                validPlans.AddRange(passingPlans);
            }

            return validPlans
                .Distinct() // tekrarı önle
                .Select(p => new TravelPlanDto
                {
                    Id = p.Id,
                    FromCityId = p.FromCityId,
                    ToCityId = p.ToCityId,
                    TravelDate = p.TravelDate,
                    Description = p.Description,
                    TotalSeats = p.TotalSeats,
                    AvailableSeats = p.AvailableSeats,
                    IsPublished = p.IsPublished
                }).ToList();
        }
    }
}
