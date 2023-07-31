using Lab12.Models.DTO;

namespace Lab12.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<HotelRoomDTO> Create(HotelRoomDTO Hotelroomdto,int HotelID);

        // GET All
        Task<List<HotelRoomDTO>> GetHotelRooms();

        // GET Hotel By Id

        Task<HotelRoomDTO> GetHotelRoom(int HotelID,int RoomNumber);

        // Update
        Task<HotelRoomDTO> UpdateHotelRoom(int HotelID,int RoomNumber, HotelRoomDTO Hotelroomdto);

        // Delete 

        Task Delete(int HotelID,int RoomNumber);

    }
}
