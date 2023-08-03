using Lab12.Data;
using Lab12.Models.DTO;
using Lab12.Models.Interfaces;
using Lab12.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace Lab12.Models.Services
{
    public class AmenityService : IAmenity
    {
        private readonly HotelContext _context;
        public AmenityService(HotelContext context)
        {
            _context = context;
            
        }



        /// <summary>
        /// this Create method adds a new record from AmenityDTO by passing it model in the patameter then we bind the props from the
        /// original model and with the DTO model, then we save the changes in the database for the new record 
        /// </summary>
        /// <param name="amenitydto"></param>
        /// <returns></returns>
        public async Task<AmenityDTO> Create(AmenityDTO amenitydto)
        {
          

            Amenity amenity = new Amenity
            {
                Id = amenitydto.Id,
                Name = amenitydto.Name
            };
            _context.Entry(amenity).State = EntityState.Added;

            await _context.SaveChangesAsync();
            amenitydto.Id = amenity.Id;

            return amenitydto;
        }


        /// <summary>
        /// this method deletes an existing record of type Amenity in the database by passing the ID in the parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            Amenity amenity = await _context.Amenities.FindAsync(id);

            _context.Entry(amenity).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// this methods retrieves all records of Amenities from the database and display all it props
        /// </summary>
        /// <returns></returns>
        public async Task<List<AmenityDTO>> GetAmenities()
        {
           

            return await _context.Amenities.Select(a => new AmenityDTO
            {
                Id = a.Id,
                Name = a.Name,

            }).ToListAsync();
        }


        /// <summary>
        /// this method retrieve a single record of type Amenity from the database by the passed ID in the parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AmenityDTO> GetAmenity(int id)
        {
            //Amenity amenity = await _context.Amenities.FindAsync(id);

            //return amenity;
            var amenity = await _context.Amenities.Select(a => new AmenityDTO
            {
                Id = a.Id,
                Name = a.Name,

            }).FirstOrDefaultAsync(x => x.Id == id);
            return amenity;


        }


        /// <summary>
        /// this method updates an existing record of type Amenity in the database by passing the ID of that record and the new model data the user input
        /// then we swap/change the old data with the new data that the user inserted
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amenity"></param>
        /// <returns></returns>
        public async Task<AmenityDTO> UpdateAmenity(int id, AmenityDTO amenity)
        {
            AmenityDTO amenityDto = new AmenityDTO
            {
                Id = amenity.Id,
                Name = amenity.Name
            };
            _context.Entry(amenity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return amenityDto;
        }
    }
}
