using Lab12.Models.DTO;

namespace Lab12.Models.Interfaces
{
    public interface IAmenity
    {
        /// <summary>
        /// this interfaces contains the basic CRUD operations for the Amenity Model
        /// </summary>
        /// <param name="amenity"></param>
        /// <returns></returns>


        Task<AmenityDTO> Create(AmenityDTO amenity);

        // GET All
        Task<List<AmenityDTO>> GetAmenities();

        // GET Amenity By Id

        Task<AmenityDTO> GetAmenity(int amenityId);

        // Update
        Task<AmenityDTO> UpdateAmenity(int id, AmenityDTO amenity);

        // Delete 

        Task Delete(int id);

    }
}
