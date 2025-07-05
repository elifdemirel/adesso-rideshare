using AdessoRideShare.DTOs;

namespace AdessoRideShare.Services.Interfaces
{
    public interface ITravelRequestService
    {
        Task<Guid> SendRequestAsync(CreateTravelRequestDto dto, Guid userId);
        Task<List<TravelRequestListItemDto>> GetPendingRequestsAsync(Guid planId);
        Task ApproveRequestAsync(Guid requestId);
        Task RejectRequestAsync(Guid requestId);
    }
}
