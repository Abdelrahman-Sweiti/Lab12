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



        /// <summary>
        /// this Create method adds a new record from Room by passing it model in the patameter then we bind the props from the
        /// original model and with the DTO model, then we save the changes in the database for the new record 
        /// </summary>
        /// <param name="amenitydto"></param>
        /// <returns></returns>
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


        /// <summary>
        /// this method deletes an existing record of type Amenity in the database by passing the ID in the parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// this method retrieve a single record of type Room from the database by the passed ID in the parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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



        /// <summary>
        /// this methods retrieves all records of Rooms from the database and display all it props
        /// </summary>
        /// <returns></returns>
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



        /// <summary>
        /// this method updates an existing record of type Room in the database by passing the ID of that record and the new model data the user input
        /// then we swap/change the old data with the new data that the user inserted
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amenity"></param>
        /// <returns></returns>
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


      
        /// <summary>
        /// this method adds an amenity record to a room by passing the RoomID and the AmenityID in the parameter
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="amenityId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// this method removes an amenity record to a room by passing the RoomID and the AmenityID in the parameter
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="amenityId"></param>
        /// <returns></returns>
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
