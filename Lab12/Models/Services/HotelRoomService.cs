using Lab12.Data;
using Lab12.Models.Interfaces;
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

        public async Task<HotelRoom> Create(HotelRoom hotelroom)
        {
            _context.HotelRoom.Add(hotelroom);
            await _context.SaveChangesAsync();
            return hotelroom;
        }

        public async Task Delete(int HotelID, int RoomID)
        {
            HotelRoom hotelroom = await GetHotelRoom(HotelID,RoomID);
            if (hotelroom != null)
            {
                _context.HotelRoom.Remove(hotelroom);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<HotelRoom> GetHotelRoom(int HotelID,int RoomID)
        {
            var HotelRoom = await _context.HotelRoom.FindAsync(HotelID,RoomID);
            return HotelRoom;
        }

        public async Task<List<HotelRoom>> GetHotelRooms()
        {
            var HotelRooms = await _context.HotelRoom.ToListAsync();
            return HotelRooms;
        }

        public async Task<HotelRoom> UpdateHotelRoom(int HotelID,int RoomID, HotelRoom hotelroom)
        {
            HotelRoom CurrentHotelRoom = await GetHotelRoom(HotelID, RoomID);


            if (CurrentHotelRoom != null)
            {

                CurrentHotelRoom.RoomNumber = hotelroom.RoomNumber;
                CurrentHotelRoom.RoomID = hotelroom.RoomID;
                CurrentHotelRoom.Rate = hotelroom.Rate;
                CurrentHotelRoom.PetFriendly= hotelroom.PetFriendly;
                await _context.SaveChangesAsync();

            }
            return CurrentHotelRoom;
        }
    }
}
