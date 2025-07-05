using AdessoRideShare.DTOs;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AdessoRideShare.SwaggerExamples
{
    public class CreateTravelPlanDtoExample : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(CreateTravelPlanDto))
            {
                schema.Example = new OpenApiObject
                {
                    ["fromCityId"] = new OpenApiInteger(1),
                    ["toCityId"] = new OpenApiInteger(2),
                    ["travelDate"] = new OpenApiString("2025-07-08T10:00:00Z"),
                    ["description"] = new OpenApiString("İzmir'den Ankara'ya gidiyorum"),
                    ["totalSeats"] = new OpenApiInteger(3)
                };
            }
        }
    }
}
