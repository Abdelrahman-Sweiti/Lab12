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



        /// <summary>
        /// this Create method adds a new record from Hotel by passing it model in the patameter then we bind the props from the
        /// original model and with the DTO model, then we save the changes in the database for the new record 
        /// </summary>
        /// <param name="amenitydto"></param>
        /// <returns></returns>
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


        /// <summary>
        /// this method deletes an existing record of type Hotel in the database by passing the ID in the parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            Hotel hotel = await _context.Hotels.FindAsync(id);

            _context.Entry(hotel).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }



        /// <summary>
        /// this method retrieve a single record of type Hotel from the database by the passed ID in the parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                            Id = hotelR.Room.Id,
                            Name = hotelR.Room.Name,
                            Layout = hotelR.Room.Layout,
                            Amenities = hotelR.Room.RoomAmenities
                            .Select(A => new AmenityDTO
                            {
                                Id = A.Amenity.Id,
                                Name = A.Amenity.Name
                            }).ToList()
                        }
                    }).ToList()
                }).FirstOrDefaultAsync(h => h.ID == id);
            return hotel;
        }


        /// <summary>
        /// this methods retrieves all records of Hotels from the database and display all it props
        /// </summary>
        /// <returns></returns>
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
                            Id = hotelR.Room.Id,
                            Name = hotelR.Room.Name,
                            Layout = hotelR.Room.Layout,
                            Amenities = hotelR.Room.RoomAmenities
                            .Select(A => new AmenityDTO
                            {
                                Id = A.Amenity.Id,
                                Name = A.Amenity.Name
                            }).ToList()
                        }
                    }).ToList()
                }).ToListAsync();
        }


        /// <summary>
        /// this method updates an existing record of type Hotel in the database by passing the ID of that record and the new model data the user input
        /// then we swap/change the old data with the new data that the user inserted
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amenity"></param>
        /// <returns></returns>
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
