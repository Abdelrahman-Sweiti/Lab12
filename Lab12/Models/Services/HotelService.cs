using Lab12.Data;
using Lab12.Models.DTO;
using Lab12.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab12.Models.Services
{
    public class HotelService : IHotel
    {

        private readonly HotelContext _context;

        public HotelService(HotelContext context)
        {
            _context = context;
        }


        public async Task<HotelDTO> Create(HotelDTO hotel)
        {

            _context.Entry(hotel).State = EntityState.Added;

            await _context.SaveChangesAsync();

            HotelDTO hotelDTO = new HotelDTO
            {
                ID = hotel.ID,
                Name = hotel.Name,
                StreetAddress = hotel.StreetAddress,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.Phone,
            };

            return hotelDTO;
        }

        public async Task Delete(int id)
        {
            Hotel hotel = await _context.Hotels.FindAsync(id);

            _context.Entry(hotel).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }


        public async Task<HotelDTO> GetHotel(int id)
        {
            var hotel =  await _context.Hotels.Select(
                hotel => new HotelDTO
                {
                    ID = hotel.Id,
                    Name = hotel.Name,
                    StreetAddress = hotel.StreetAddress,
                    City = hotel.City,
                    State = hotel.State,
                    Phone = hotel.Phone,
                    Rooms = hotel.HotelRooms.Select(hotelR => new HotelRoomDTO
                    {
                        HotelID = hotelR.HotelID,
                        RoomNumber = hotelR.RoomNumber,
                        Rate = hotelR.Rate,
                        PetFriendly = hotelR.PetFriendly,
                        RoomID = hotelR.RoomID,
                        Room = new RoomDTO
                        {
                            ID = hotelR.Room.Id,
                            Name = hotelR.Room.Name,
                            Layout = hotelR.Room.Layout,
                            Amenities = hotelR.Room.RoomAmenities
                            .Select(A => new AmenityDTO
                            {
                                ID = A.Amenity.Id,
                                Name = A.Amenity.Name
                            }).ToList()
                        }
                    }).ToList()
                }).FirstOrDefaultAsync(h => h.ID == id);
            return hotel;
        }

        public async Task<List<HotelDTO>> GetHotels()
        {
            
            return await _context.Hotels.Select(
                hotel => new HotelDTO
                {
                    ID = hotel.Id,
                    Name = hotel.Name,
                    StreetAddress = hotel.StreetAddress,
                    City = hotel.City,
                    State = hotel.State,
                    Phone = hotel.Phone,
                    Rooms = hotel.HotelRooms.Select(hotelR => new HotelRoomDTO
                    {
                        HotelID = hotelR.HotelID,
                        RoomNumber = hotelR.RoomNumber,
                        Rate = hotelR.Rate,
                        PetFriendly = hotelR.PetFriendly,
                        RoomID = hotelR.RoomID,
                        Room = new RoomDTO
                        {
                            ID = hotelR.Room.Id,
                            Name = hotelR.Room.Name,
                            Layout = hotelR.Room.Layout,
                            Amenities = hotelR.Room.RoomAmenities
                            .Select(A => new AmenityDTO
                            {
                                ID = A.Amenity.Id,
                                Name = A.Amenity.Name
                            }).ToList()
                        }
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<HotelDTO> UpdateHotel(int id, HotelDTO hotel)
        {
            HotelDTO hotelDTO = new HotelDTO
            {
                ID = hotel.ID,
                Name = hotel.Name,
                StreetAddress = hotel.StreetAddress,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.Phone,
            };

            _context.Entry(hotel).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return hotelDTO;
        }
    }
}
