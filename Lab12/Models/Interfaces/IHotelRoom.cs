using Lab12.Models.DTO;

namespace Lab12.Models.Interfaces
{
    public interface IHotelRoom
    {
        /// <summary>
        /// this interfaces contains the basic CRUD operations for the HotelRoom Model
        /// </summary>
        /// <param name="amenity"></param>
        /// <returns></returns>

        Task<HotelRoomDTO> Create(HotelRoomDTO Hotelroomdto,int HotelID);

        // GET All
        Task<List<HotelRoomDTO>> GetHotelRooms(int id);

        // GET Hotel By Id

        Task<HotelRoomDTO> GetHotelRoom(int HotelID, int RoomID);

        // Update
        Task<HotelRoomDTO> UpdateHotelRoom(int hotelId, int roomNumber, HotelRoomDTO hr);

        // Delete 

        Task Delete(int HotelID,int RoomNumber);

    }
}
