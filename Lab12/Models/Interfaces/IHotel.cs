using Lab12.Models.DTO;

namespace Lab12.Models.Interfaces
{
    public interface IHotel
    {

        Task<HotelDTO> Create(Hotel Hoteldto);

        // GET All
        Task<List<HotelDTO>> GetHotels();

        // GET Hotel By Id

        Task<HotelDTO> GetHotel(int HotelId);

        // Update
        Task<HotelDTO> UpdateHotel(int id, Hotel Hoteldto);

        // Delete 

        Task Delete(int id);
    }
}
