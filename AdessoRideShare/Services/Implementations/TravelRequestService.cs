using AdessoRideShare.DTOs;
using AdessoRideShare.Models;
using AdessoRideShare.Repositories.Interfaces;
using AdessoRideShare.Services.Interfaces;

namespace AdessoRideShare.Services.Implementations
{
    public class TravelRequestService : ITravelRequestService
    {
        private readonly ITravelRequestRepository _requestRepo;
        private readonly ITravelPlanRepository _planRepo;

        public TravelRequestService(ITravelRequestRepository requestRepo, ITravelPlanRepository planRepo)
        {
            _requestRepo = requestRepo;
            _planRepo = planRepo;
        }

        public async Task<Guid> SendRequestAsync(CreateTravelRequestDto dto, Guid userId)
        {
            var plan = await _planRepo.GetByIdAsync(dto.TravelPlanId);
            if (plan == null || !plan.IsPublished)
                throw new Exception("Geçerli bir seyahat planı bulunamadı.");

            var approvedCount = await _requestRepo.GetApprovedRequestCountAsync(plan.Id);

            if (approvedCount >= plan.TotalSeats)
                throw new Exception("Tüm koltuklar dolu.");

            var request = new TravelRequest
            {
                Id = Guid.NewGuid(),
                TravelPlanId = plan.Id,
                UserId = userId,
                RequestDate = DateTime.UtcNow,
                IsApproved = false,
                Message = dto.Message
            };

            await _requestRepo.AddAsync(request);
            await _requestRepo.SaveChangesAsync();

            return request.Id;
        }

        public async Task<List<TravelRequestListItemDto>> GetPendingRequestsAsync(Guid planId)
        {
            var list = await _requestRepo.GetRequestsByPlanIdAsync(planId);
            return list
                .Where(r => !r.IsApproved)
                .Select(r => new TravelRequestListItemDto
                {
                    RequestId = r.Id,
                    UserId = r.UserId,
                    Message = r.Message,
                    RequestDate = r.RequestDate
                }).ToList();
        }

        public async Task ApproveRequestAsync(Guid requestId)
        {
            var request = await _requestRepo.GetByIdAsync(requestId);
            if (request == null)
                throw new Exception("İstek bulunamadı.");

            if (request.IsApproved)
                throw new Exception("Bu istek zaten onaylanmış.");

            var plan = await _planRepo.GetByIdAsync(request.TravelPlanId);
            if (plan == null || !plan.IsPublished)
                throw new Exception("Seyahat planı bulunamadı veya yayında değil.");

            var approvedCount = await _requestRepo.GetApprovedRequestCountAsync(plan.Id);
            if (approvedCount >= plan.TotalSeats)
                throw new Exception("Onaylanamaz. Tüm koltuklar dolu.");

            request.IsApproved = true;
            await _requestRepo.SaveChangesAsync();
        }

        public async Task RejectRequestAsync(Guid requestId)
        {
            var request = await _requestRepo.GetByIdAsync(requestId);
            if (request == null)
                throw new Exception("İstek bulunamadı.");

            await _requestRepo.RemoveAsync(request);
            await _requestRepo.SaveChangesAsync();
        }
    }
}
