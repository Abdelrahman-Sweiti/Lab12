namespace Lab12.Models.Interfaces
{
    public interface IRoom
    {
        Task<Room> Create(Room room);

        // GET All
        Task<List<Room>> GetRooms();

        // GET Room By Id

        Task<Room> GetRoom(int roomId);

        // Update
        Task<Room> UpdateRoom(int id, Room room);

        // Delete 

        Task Delete(int id);


        // Add Amenity To Room
        Task AddAmenityToRoom(int roomId, int amenityId);

        // Remove Amentity From Room
        Task RemoveAmentityFromRoom(int roomId, int amenityId);

    }
}
