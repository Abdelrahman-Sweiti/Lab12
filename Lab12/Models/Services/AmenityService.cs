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

        public async Task Delete(int id)
        {
            Amenity amenity = await _context.Amenities.FindAsync(id);

            _context.Entry(amenity).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<List<AmenityDTO>> GetAmenities()
        {
           

            return await _context.Amenities.Select(a => new AmenityDTO
            {
                Id = a.Id,
                Name = a.Name,

            }).ToListAsync();
        }

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
