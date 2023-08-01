using Humanizer;
using Lab12.Data;
using Lab12.Models.DTO;
using Lab12.Models.Interfaces;
using Lab12.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace Lab12.Models.Services
{
    public class HotelRoomService : IHotelRoom
    {
        private readonly HotelContext _context;
        private readonly IHotelRoom _hotelRoom;
        public HotelRoomService(HotelContext context,IHotelRoom hotelroom)
        {
            _context = context;
            _hotelRoom = hotelroom;
        }

        public async Task<HotelRoomDTO> Create( HotelRoom hotelRoom, int hotelId)
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

        public async Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber)
        {
            HotelRoom roomDetails = await _context.HotelRoom
                .Where(hr => hr.HotelID == hotelId && hr.RoomNumber == roomNumber)
                .FirstAsync();

            HotelRoom hotelRoom = await _context.HotelRoom.Include(r => r.Room)
                                                           .ThenInclude(am => am.RoomAmenities)
                                                           .ThenInclude(a => a.Amenity)
                                                           .Where(h => h.HotelID == roomDetails.HotelID && h.RoomID == roomDetails.RoomID)
                                                           .FirstAsync();
            return hotelRoom;
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
