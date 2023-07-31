using Lab12.Data;
using Lab12.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Lab12.Models.DTO

{
    public class RoomService : IRoom
    {
        private readonly IRoom _room;
        private readonly HotelContext _context;

        public RoomService(HotelContext context,IRoom room)
        {
            _context = context;
            _room = room;
        }


        public async Task<RoomDTO> Create(RoomDTO roomdto)
        {
            RoomDTO roomdto = new RoomDTO()
            {
                ID = roomdto.ID,
                Name = roomdto.Name,
                Layout = roomdto.Layout,


            };

             await _context.SaveChangesAsync();
            return room;
        }

        public async Task Delete(int id)
        {
           Room room = await GetRoom(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Room> GetRoom(int roomId)
        {
            var Room = await _context.Rooms.Include(x => x.RoomAmenities).ThenInclude(y=>y.Amenity).FirstOrDefaultAsync(x=>x.Id==roomId);
            return Room;
        }

        public async Task<List<Room>> GetRooms()
        {
            var Rooms = await _context.Rooms.Include(x=>x.RoomAmenities).ThenInclude(y=>y.Amenity).ToListAsync();
            return Rooms;
        }

        public async Task<Room> UpdateRoom(int id, Room UpdatedRoom)
        {
           Room CurrentRoom = await GetRoom(id);

            if (CurrentRoom != null)
            {
                CurrentRoom.Name = UpdatedRoom.Name;
                CurrentRoom.Layout = UpdatedRoom.Layout;
                await _context.SaveChangesAsync();


            }
            return CurrentRoom;

        }


      
        public async Task AddAmenityToRoom(int roomId, int amenityId)
        {
            RoomAmenity newRoomAmenity = new RoomAmenity()
            {
                RoomID = roomId,
                AmenityID = amenityId
            };
            _context.Entry(newRoomAmenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

      
        public async Task RemoveAmentityFromRoom(int roomId, int amenityId)
        {
            var removeAmentity = _context.RoomAmenities.FirstOrDefaultAsync(x => x.RoomID == roomId && x.AmenityID == amenityId);
            _context.Entry(removeAmentity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

    }
}
