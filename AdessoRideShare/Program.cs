using AdessoRideShare.Data;
using Microsoft.EntityFrameworkCore;
using AdessoRideShare.Repositories.Interfaces;
using AdessoRideShare.Repositories.Implementations;
using AdessoRideShare.Services.Interfaces;
using AdessoRideShare.Services.Implementations;
using AdessoRideShare.SwaggerExamples;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RideShareDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITravelPlanRepository, TravelPlanRepository>();
builder.Services.AddScoped<ITravelPlanService, TravelPlanService>();
builder.Services.AddScoped<ITravelRequestRepository, TravelRequestRepository>();
builder.Services.AddScoped<ITravelRequestService, TravelRequestService>();
builder.Services.AddScoped<ICityRepository, CityRepository>();

builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<CreateTravelPlanDtoExample>();
    c.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

