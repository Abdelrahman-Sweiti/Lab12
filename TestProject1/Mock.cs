using Lab12.Data;
using Lab12.Models;
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



        protected async Task<Room> CreateAndSaveTestRoom()
        {
            var room = new Room() {Name="Test1",Layout=1 };
            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();

            Assert.NotEqual(0, room.Id);

            return room;
        
        }

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
