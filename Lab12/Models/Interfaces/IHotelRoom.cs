namespace Lab12.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<HotelRoom> Create(HotelRoom hotelroom);

        // GET All
        Task<List<HotelRoom>> GetHotelRooms();

        // GET Hotel By Id

        Task<HotelRoom> GetHotelRoom(int HotelID,int RoomID);

        // Update
        Task<HotelRoom> UpdateHotelRoom(int HotelID,int RoomID, HotelRoom hotelroom);

        // Delete 

        Task Delete(int HotelID,int RoomID);

    }
}
