using Lab12.Data;
using Lab12.Models.Interfaces;
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


        public async Task<Amenity> Create(Amenity amenity)
        {
            _context.Amenities.Add(amenity);
            await _context.SaveChangesAsync();
            return amenity;
        }

        public async Task Delete(int id)
        {
            Amenity amenity = await GetAmenity(id);
            if (amenity != null)
            {
                _context.Amenities.Remove(amenity);
                await _context.SaveChangesAsync();


            }
        }

        public async Task<List<Amenity>> GetAmenities()
        {
           var Amenities = await _context.Amenities.Include(x=>x.RoomAmenities).ToListAsync();
            return Amenities;
        }

        public async Task<Amenity> GetAmenity(int amenityId)
        {
            var Amenity = await _context.Amenities.Include(x => x.RoomAmenities).FirstOrDefaultAsync(x => x.Id == amenityId);
            return Amenity;
        }

        public async Task<Amenity> UpdateAmenity(int id, Amenity UpdatedAmenity)
        {
            Amenity CurrentAmenity = await GetAmenity(id);

            if (CurrentAmenity != null)
            {
                CurrentAmenity.Name = UpdatedAmenity.Name;
                await _context.SaveChangesAsync();


            }
            return CurrentAmenity;
        }
    }
}
