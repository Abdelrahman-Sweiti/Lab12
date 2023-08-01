using Humanizer;
using Lab12.Data;
using Lab12.Models.DTO;
using Lab12.Models.Interfaces;
using Lab12.Models.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Lab12.Models.Services
{
    public class HotelRoomService : IHotelRoom
    {
        private readonly HotelContext _context;
        
        public HotelRoomService(HotelContext context)
        {
            _context = context;
            
        }

        public async Task<HotelRoomDTO> Create( HotelRoomDTO hotelRoom, int hotelId)
        {
            _context.Entry(hotelRoom).State = EntityState.Added;

            await _context.SaveChangesAsync();

            HotelRoomDTO room = new HotelRoomDTO
            {
                HotelID = hotelId,
                RoomNumber = hotelRoom.RoomNumber,
                Rate = hotelRoom.RoomID,
                PetFriendly = hotelRoom.PetFriendly,
                RoomID = hotelRoom.RoomID
            };

            return room;
        }

        public async Task Delete(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRoom
                .Where(hr => hr.HotelID == hotelId && hr.RoomNumber == roomNumber)
                .FirstAsync();

            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomNumber)
        {
            var hotelroom = await _context.HotelRoom
               .Select(hr => new HotelRoomDTO
               {
                   HotelID = hr.HotelID,
                   Rate = hr.Rate,
                   RoomID = hr.RoomID,
                   RoomNumber = hr.RoomNumber,
                   Room = new RoomDTO
                   {
                       ID = hr.Room.Id,
                       Name = hr.Room.Name,
                       Layout = hr.Room.Layout,
                       Amenities = hr.Room.RoomAmenities
                           .Select(a => new AmenityDTO
                           {
                               ID = a.Amenity.Id,
                               Name = a.Amenity.Name
                           }).ToList()
                   }
               }).FirstOrDefaultAsync(x=> x.HotelID == hotelId &&  x.RoomNumber == roomNumber);
            return hotelroom;
        }

        public async Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId)
        {
            return await _context.HotelRoom
               .Where(hr => hr.HotelID == hotelId)
               .Select(hr => new HotelRoomDTO
               {
                   HotelID = hr.HotelID,
                   Rate = hr.Rate,
                   RoomID = hr.RoomID,
                   RoomNumber = hr.RoomNumber,
                   Room = new RoomDTO
                   {
                       ID = hr.Room.Id,
                       Name = hr.Room.Name,
                       Layout = hr.Room.Layout,
                       Amenities = hr.Room.RoomAmenities
                           .Select(a => new AmenityDTO
                           {
                               ID = a.Amenity.Id,
                               Name = a.Amenity.Name
                           }).ToList()
                   }
               }).ToListAsync();
        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(int hotelId, int roomNumber, HotelRoomDTO hr)
        {
            HotelRoom roomDetails = new HotelRoom
            {
                HotelID = hotelId,
                RoomNumber = roomNumber,
                RoomID = hr.RoomID,
                Rate = hr.Rate,
                PetFriendly = hr.PetFriendly
            };

            _context.Entry(roomDetails).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return hr;
        }
    }
}
