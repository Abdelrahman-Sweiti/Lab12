using Lab12.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab12.Data
{
    public class HotelContext : DbContext
    {

        public HotelContext(DbContextOptions options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel() { Id = 1, Name = "Lux Room",StreetAddress="TV-Street",City="Amman",Country="Jordan",Phone="0796153883",State="Middle-East" }
                
                );

            modelBuilder.Entity<Room>().HasData(
                new Room() { Id=1,Name="Luxury Room",Layout=1 });


            modelBuilder.Entity<Amenities>().HasData(
                new Amenities() {Id=1,Name="Fancy" });

        }





        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<HotelRoom> HotelRoom { get; set; }
        public DbSet<RoomAmenities> RoomAmenities { get; set; }
        public DbSet<Amenities> Amenities { get; set; }

    }
}
