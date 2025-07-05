using System;
using AdessoRideShare.Models;
using Microsoft.EntityFrameworkCore;

namespace AdessoRideShare.Data
{
    public class RideShareDbContext : DbContext
    {
        public RideShareDbContext(DbContextOptions<RideShareDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<TravelPlan> TravelPlans { get; set; }
        public DbSet<TravelRequest> TravelRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ➤ TravelPlan → FromCity
            modelBuilder.Entity<TravelPlan>()
                .HasOne(tp => tp.FromCity)
                .WithMany()
                .HasForeignKey(tp => tp.FromCityId)
                .OnDelete(DeleteBehavior.Restrict);

            // ➤ TravelPlan → ToCity
            modelBuilder.Entity<TravelPlan>()
                .HasOne(tp => tp.ToCity)
                .WithMany()
                .HasForeignKey(tp => tp.ToCityId)
                .OnDelete(DeleteBehavior.Restrict);

            // ➤ TravelPlan → User
            modelBuilder.Entity<TravelPlan>()
                .HasOne(tp => tp.User)
                .WithMany()
                .HasForeignKey(tp => tp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ➤ TravelRequest → TravelPlan
            modelBuilder.Entity<TravelRequest>()
                .HasOne(tr => tr.TravelPlan)
                .WithMany()
                .HasForeignKey(tr => tr.TravelPlanId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid multiple cascade paths

            // ➤ TravelRequest → User
            modelBuilder.Entity<TravelRequest>()
                .HasOne(tr => tr.User)
                .WithMany()
                .HasForeignKey(tr => tr.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid multiple cascade paths

            // 50km'lik kareler = 20x10 grid (X: 0-19, Y: 0-9)
            modelBuilder.Entity<City>().HasData(
                // Ana şehirler - gerçekçi grid pozisyonları
                new City { Id = 1, Name = "İzmir", GridX = 2, GridY = 8 },          // Batı kıyı
                new City { Id = 2, Name = "Ankara", GridX = 10, GridY = 5 },        // Merkez
                new City { Id = 3, Name = "İstanbul", GridX = 6, GridY = 2 },       // Kuzey
                new City { Id = 4, Name = "Manisa", GridX = 3, GridY = 7 },         // İzmir yakını
                new City { Id = 5, Name = "Uşak", GridX = 5, GridY = 6 },           // İç bölge
                new City { Id = 6, Name = "Afyon", GridX = 8, GridY = 6 },          // Merkez-batı
                new City { Id = 7, Name = "Eskişehir", GridX = 9, GridY = 4 },      // Merkez-kuzey
                new City { Id = 8, Name = "Bursa", GridX = 5, GridY = 3 },          // Kuzey-batı
                new City { Id = 9, Name = "Kütahya", GridX = 6, GridY = 5 },        // Batı-merkez
                new City { Id = 10, Name = "Balıkesir", GridX = 4, GridY = 4 },     // Batı
                new City { Id = 11, Name = "Kocaeli", GridX = 7, GridY = 1 },       // Kuzey-doğu
                new City { Id = 12, Name = "Sakarya", GridX = 8, GridY = 2 },       // Kuzey-merkez
                new City { Id = 13, Name = "Denizli", GridX = 4, GridY = 8 },       // Güney-batı
                new City { Id = 14, Name = "Konya", GridX = 12, GridY = 7 },        // Merkez-doğu
                new City { Id = 15, Name = "Antalya", GridX = 8, GridY = 9 }        // Güney
            );

            modelBuilder.Entity<TravelPlan>().HasData(
                 new TravelPlan
                 {
                     Id = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                     UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                     FromCityId = 1, // İzmir
                     ToCityId = 2,   // Ankara
                     TravelDate = DateTime.UtcNow.AddDays(1),
                     Description = "İzmir'den Ankara'ya",
                     TotalSeats = 4,
                     AvailableSeats = 4,
                     IsPublished = true
                 },
                 new TravelPlan
                 {
                     Id = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                     UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                     FromCityId = 4, // Manisa
                     ToCityId = 7,   // Eskişehir
                     TravelDate = DateTime.UtcNow.AddDays(2),
                     Description = "Manisa'dan Eskişehir'e",
                     TotalSeats = 3,
                     AvailableSeats = 3,
                     IsPublished = true
                 },
                 new TravelPlan
                 {
                     Id = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                     UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                     FromCityId = 8, // Bursa
                     ToCityId = 12,  // Sakarya
                     TravelDate = DateTime.UtcNow.AddDays(3),
                     Description = "Bursa'dan Sakarya'ya",
                     TotalSeats = 2,
                     AvailableSeats = 2,
                     IsPublished = true
                 }
             );

            // ➤ Seed User Data (test user)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    FullName = "Test Kullanıcı",
                    Email = "test@adesso.com",
                    PasswordHash = "hashedpassword"
                }
            );
        }
    }
}
