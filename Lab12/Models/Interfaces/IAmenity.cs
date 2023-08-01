using Lab12.Models.DTO;

namespace Lab12.Models.Interfaces
{
    public interface IAmenity
    {
        Task<AmenityDTO> Create(Amenity amenity);

        // GET All
        Task<List<AmenityDTO>> GetAmenities();

        // GET Amenity By Id

        Task<AmenityDTO> GetAmenity(int amenityId);

        // Update
        Task<AmenityDTO> UpdateAmenity(int id, Amenity amenity);

        // Delete 

        Task Delete(int id);

    }
}
