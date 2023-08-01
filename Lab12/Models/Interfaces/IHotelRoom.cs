using Lab12.Models.DTO;

namespace Lab12.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<HotelRoomDTO> Create(HotelRoom Hotelroomdto,int HotelID);

        // GET All
        Task<List<HotelRoomDTO>> GetHotelRooms(int id);

        // GET Hotel By Id

        Task<HotelRoom> GetHotelRoom(int HotelID, int RoomID);

        // Update
        Task<HotelRoomDTO> UpdateHotelRoom(int hotelId, int roomNumber, HotelRoomDTO hr);

        // Delete 

        Task Delete(int HotelID,int RoomNumber);

    }
}
