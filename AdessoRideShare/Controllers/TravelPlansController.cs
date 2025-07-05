using Microsoft.AspNetCore.Mvc;
using AdessoRideShare.DTOs;
using AdessoRideShare.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace AdessoRideShare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TravelPlansController : ControllerBase
    {
        private readonly ITravelPlanService _travelPlanService;

        public TravelPlansController(ITravelPlanService travelPlanService)
        {
            _travelPlanService = travelPlanService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Yeni bir seyahat planı oluşturur.")]
        public async Task<IActionResult> CreateTravelPlan([FromBody] CreateTravelPlanDto dto)
        {
            var userId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            try
            {
                var planId = await _travelPlanService.CreateTravelPlanAsync(dto, userId);
                return Ok(ApiResponse<object>.Ok(new { id = planId }, "Seyahat planı oluşturuldu."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.Fail(ex.Message));
            }
        }

        [HttpPut("{id}/publish")]
        [SwaggerOperation(Summary = "Yolculuk planını yayına alır veya yayından kaldırır.")]
        public async Task<IActionResult> PublishPlan(Guid id, [FromQuery] bool publish)
        {
            try
            {
                await _travelPlanService.PublishTravelPlanAsync(id, publish);
                return Ok(ApiResponse<object>.Ok(null, publish ? "Plan yayına alındı." : "Plan yayından kaldırıldı."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.Fail(ex.Message));
            }
        }

        [HttpGet("search")]
        [SwaggerOperation(Summary = "Yayındaki planları kalkış ve varış şehirlerine göre arar.")]
        public async Task<IActionResult> GetTravelPlans(int? fromCityId, int? toCityId)
        {
            try
            {
                var results = await _travelPlanService.GetPublishedPlansAsync(fromCityId, toCityId);
                return Ok(ApiResponse<object>.Ok(results, "Planlar listelendi."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.Fail(ex.Message));
            }
        }


        [HttpGet("passingthrough")]
        [SwaggerOperation(Summary = "Güzergâhtan geçen yayınlanmış tüm planları listeler.")]
        public async Task<IActionResult> GetPlansPassingThrough([FromQuery] int fromCityId, [FromQuery] int toCityId)
        {
            try
            {
                var results = await _travelPlanService.GetPlansPassingThroughAsync(fromCityId, toCityId);
                return Ok(ApiResponse<object>.Ok(results, "Geçilen planlar listelendi."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.Fail(ex.Message));
            }
        }
    }
}
