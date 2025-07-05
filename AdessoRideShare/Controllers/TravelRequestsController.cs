using AdessoRideShare.DTOs;
using AdessoRideShare.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AdessoRideShare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TravelRequestsController : ControllerBase
    {
        private readonly ITravelRequestService _requestService;

        public TravelRequestsController(ITravelRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Yayındaki bir seyahat planına katılım isteği gönderir.")]
        public async Task<IActionResult> SendRequest([FromBody] CreateTravelRequestDto dto)
        {
            var userId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            try
            {
                var requestId = await _requestService.SendRequestAsync(dto, userId);
                return Ok(ApiResponse<object>.Ok(new { id = requestId }, "Talep başarıyla oluşturuldu."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.Fail(ex.Message));
            }
        }

        [HttpGet("pending/{planId}")]
        [SwaggerOperation(Summary = "Belirli bir seyahat planına ait onaylanmamış tüm talepleri getirir.")]
        public async Task<IActionResult> GetPendingRequests(Guid planId)
        {
            try
            {
                var pending = await _requestService.GetPendingRequestsAsync(planId);
                return Ok(ApiResponse<object>.Ok(pending, "Bekleyen istekler listelendi."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.Fail(ex.Message));
            }
        }

        [HttpPost("{requestId}/approve")]
        [SwaggerOperation(Summary = "Bir seyahat isteğini onaylar.")]
        public async Task<IActionResult> ApproveRequest(Guid requestId)
        {
            try
            {
                await _requestService.ApproveRequestAsync(requestId);
                return Ok(ApiResponse<object>.Ok(null, "İstek onaylandı."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.Fail(ex.Message));
            }
        }

        [HttpPost("{requestId}/reject")]
        [SwaggerOperation(Summary = "Bir seyahat isteğini reddeder.")]
        public async Task<IActionResult> RejectRequest(Guid requestId)
        {
            try
            {
                await _requestService.RejectRequestAsync(requestId);
                return Ok(ApiResponse<object>.Ok(null, "İstek reddedildi."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.Fail(ex.Message));
            }
        }
    }
}
