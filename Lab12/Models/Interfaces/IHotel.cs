using Lab12.Models.DTO;

namespace Lab12.Models.Interfaces
{
    public interface IHotel
    {
        /// <summary>
        /// this interfaces contains the basic CRUD operations for the Hotel Model
        /// </summary>
        /// <param name="amenity"></param>
        /// <returns></returns>
        Task<Hotel> Create(Hotel hotel);

        // GET All
        Task<List<HotelDTO>> GetHotels();

        // GET Hotel By Id

        Task<HotelDTO> GetHotel(int HotelId);

        // Update
        Task<Hotel> UpdateHotel(int id, Hotel hotel);

        // Delete 

        Task Delete(int id);
    }
}
