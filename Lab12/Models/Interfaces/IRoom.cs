using Lab12.Models.DTO;

namespace Lab12.Models.Interfaces
{
    public interface IRoom
    {
        Task<RoomDTO> Create(Room Roomdto);

        // GET All
        Task<List<RoomDTO>> GetRooms();

        // GET Room By Id

        Task<RoomDTO> GetRoom(int roomId);

        // Update
        Task<RoomDTO> UpdateRoom(int id, Room Roomdto);

        // Delete 

        Task Delete(int id);


        // Add Amenity To Room
        Task AddAmenityToRoom(int roomId, int amenityId);

        // Remove Amentity From Room
        Task RemoveAmentityFromRoom(int roomId, int amenityId);

    }
}
