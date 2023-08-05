using Lab12.Data;
using Lab12.Models;
using Lab12.Models.DTO;
using Lab12.Models.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly HotelContext _db;
        protected readonly IAmenity _am;
        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new HotelContext(new DbContextOptionsBuilder<HotelContext>().UseSqlite(_connection).Options);

            _db.Database.EnsureCreated();
        }

        //protected async Task<RoomDTO> CreateDataBase()
        //{
        //    var room = new Room()
        //    {

        //        Name = "Test1",
        //        Layout = 1


        //    };

           
        //    return room;

        //}



        protected async Task<Room> CreateAndSaveTestRoom()
        {
            var room = new Room() {Name="Test1",Layout=1 };
            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();

            Assert.NotEqual(0, room.Id);

            return room;
        
        }

        protected async Task<Room> DeleteAndSaveRoomTest()
        {
            var room = new Room() { Id=1,Name = "Test1", Layout = 1 };
             _db.Rooms.Remove(room);
            await _db.SaveChangesAsync();

            var deletedRoom = await _db.Rooms.FindAsync(room.Id);
            Assert.Null(deletedRoom);

            return room;
        }


        protected async Task<Amenity> DeleteAndSaveAmenityTest()
        {
            var amenity = new Amenity() { Id = 1, Name = "Test1"};
            _db.Amenities.Remove(amenity);
            await _db.SaveChangesAsync();

            var deletedAmenity = await _db.Amenities.FindAsync(amenity.Id);
            Assert.Null(deletedAmenity);

            return amenity;
        }


        //protected async Task<RoomDTO> CreateRoomTest()
        //{

        //    var room = new Room() {
        //        Id = 1,
        //        Name = "Test1",
        //        Layout = 1

        //    };

        //    _db.Rooms.Add(room);
        //    await _db.SaveChangesAsync();
        //    return room;
        
        //}


        //protected async Task<Hotel> CreateAndSaveTestHotel()
        //{
        //    var hotel = new Hotel() { Id = 1, Name = "Lux Room", StreetAddress = "TV-Street", City = "Amman", Country = "Jordan", Phone = "0796153883", State = "Middle-East" };
        //    _db.Hotels.Add(hotel);
        //    await _db.SaveChangesAsync();

        //    Assert.NotEqual(0, hotel.Id);

        //    return hotel;

        //}

        protected async Task<Amenity> CreateAndSaveTestAmenity()
        {
            var amenity = new Amenity() { Name = "Test2"};
            _db.Amenities.Add(amenity);
            await _db.SaveChangesAsync();

            Assert.NotEqual(0, amenity.Id);

            return amenity;

        }



        public void Dispose()
        {
           
            _db?.Dispose();
        _connection.Dispose();

        
        }
    }
}
