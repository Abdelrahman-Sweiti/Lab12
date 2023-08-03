﻿using Lab12.Models.DTO;

namespace Lab12.Models.Interfaces
{
    public interface IHotel
    {
        /// <summary>
        /// this interfaces contains the basic CRUD operations for the Hotel Model
        /// </summary>
        /// <param name="amenity"></param>
        /// <returns></returns>
        Task<HotelDTO> Create(HotelDTO Hoteldto);

        // GET All
        Task<List<HotelDTO>> GetHotels();

        // GET Hotel By Id

        Task<HotelDTO> GetHotel(int HotelId);

        // Update
        Task<HotelDTO> UpdateHotel(int id, HotelDTO Hoteldto);

        // Delete 

        Task Delete(int id);
    }
}
