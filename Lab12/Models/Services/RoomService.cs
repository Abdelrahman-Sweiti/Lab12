using Lab12.Data;
using Lab12.Models.Interfaces;
using Lab12.Models.Services;
using Microsoft.EntityFrameworkCore;
namespace Lab12.Models.DTO

{
    public class RoomService : IRoom
    {
        private readonly HotelContext _context;
        private readonly IAmenity _amenity;
        public RoomService(HotelContext context,IAmenity amenity)
        {
            _context = context;
            _amenity = amenity;
        }


        public async Task<RoomDTO> Create(AddNewRoomDTO Newroom)
        {

         

            Room room = new Room
            {
                Id = Newroom.Id,
                Name = Newroom.Name,
                Layout = Newroom.Layout

            };
            _context.Rooms.Entry(room).State=EntityState.Added;

            await _context.SaveChangesAsync();
            Newroom.Id = room.Id;
            var x = await _amenity.GetAmenity(Newroom.AmenityID);

            await AddAmenityToRoom(room.Id,x.Id);
            RoomDTO dto = await GetRoom(Newroom.Id);

            return dto;
        }

        public async Task Delete(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<RoomDTO> GetRoom(int roomId)
        {
            var room =  await _context.Rooms.Select(r => new RoomDTO
            {
                Id = r.Id,
                Name = r.Name,
                Layout = r.Layout,
                Amenities = r.RoomAmenities.Select(a => new AmenityDTO
                {
                    Id = a.Amenity.Id,
                    Name = a.Amenity.Name
                }).ToList()
            }).FirstOrDefaultAsync(x => x.Id == roomId);
            return room;
        }

        public async Task<List<RoomDTO>> GetRooms()
        {
            return await _context.Rooms.Select(r => new RoomDTO
            {
                Id = r.Id,
                Name = r.Name,
                Layout = r.Layout,
                Amenities = r.RoomAmenities.Select(a => new AmenityDTO
                {
                    Id = a.Amenity.Id,
                    Name = a.Amenity.Name
                }).ToList()
            }).ToListAsync();
        }

        public async Task<RoomDTO> UpdateRoom(int id, RoomDTO UpdatedRoom)
        {
            RoomDTO roomDTO = new RoomDTO
            {
                Id = UpdatedRoom.Id,
                Name = UpdatedRoom.Name,
                Layout = UpdatedRoom.Layout
            };

            _context.Entry(UpdatedRoom).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return roomDTO;

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

        //public Task<RoomDTO> UpdateRoom(int id, RoomDTO Roomdto)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
