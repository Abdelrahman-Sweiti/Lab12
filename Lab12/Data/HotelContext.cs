﻿using Lab12.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab12.Data
{
    public class HotelContext : IdentityDbContext<ApplicationUser>
    {

        public HotelContext(DbContextOptions options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Hotel>().HasData(
                new Hotel() { Id = 1, Name = "Lux Room", StreetAddress = "TV-Street", City = "Amman", Country = "Jordan", Phone = "0796153883", State = "Middle-East" },
                new Hotel() { Id = 2, Name = "Grand Hotel", StreetAddress = "Central Avenue", City = "New York", Country = "USA", Phone = "1234567890", State = "North America" },
                new Hotel() { Id = 3, Name = "Seaside Resort", StreetAddress = "Beach Road", City = "Sydney", Country = "Australia", Phone = "9876543210", State = "Oceania" }
            );

            modelBuilder.Entity<Room>().HasData(
                new Room() { Id = 1, Name = "Luxury Room", Layout = 1 },
                new Room() { Id = 2, Name = "Standard Room", Layout = 2 },
                new Room() { Id = 3, Name = "Penthouse Suite", Layout = 3 }
            );

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity() { Id = 1, Name = "Fancy" },
                new Amenity() { Id = 2, Name = "Free Wi-Fi" },
                new Amenity() { Id = 3, Name = "Swimming Pool" }
            );



            SeedRole(modelBuilder, "District Manager", "create", "update", "delete");
            SeedRole(modelBuilder, "Property Manager", "create", "update");
            SeedRole(modelBuilder, "Agent", "create", "update", "delete");


            modelBuilder.Entity<RoomAmenity>().HasKey(
                RoomAmenities => new
                {
                    RoomAmenities.AmenityID,
                    RoomAmenities.RoomID
                }
                );


            modelBuilder.Entity<HotelRoom>().HasKey(
               HotelRooms => new {
                   HotelRooms.HotelID,
                   HotelRooms.RoomNumber


               });


        }




        private int id = 1;
        private void SeedRole(ModelBuilder modelBuilder, string roleName, params string[] permissions)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };
            modelBuilder.Entity<IdentityRole>().HasData(role);

            // Go through the permissions list and seed a new entry for each
            var roleClaims = permissions.Select(permission =>
             new IdentityRoleClaim<string>
             {
                 Id = id++,
                 RoleId = role.Id,
                 ClaimType = "permissions",
                 ClaimValue = permission
             }
            );
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(roleClaims);
        }



        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<HotelRoom> HotelRoom { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }


    }
}
