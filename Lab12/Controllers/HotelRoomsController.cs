using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab12.Data;
using Lab12.Models;
using Lab12.Models.Interfaces;
using Lab12.Models.DTO;

namespace Lab12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _hotelroom;

        public HotelRoomsController(IHotelRoom hotelroom)
        {
            _hotelroom = hotelroom;
        }

        // GET: api/HotelRooms
        [HttpGet("{HotelID}/Rooms")]
        public async Task<ActionResult<IEnumerable<HotelRoomDTO>>> GetHotelRooms(int HotelID)
        {
            return await _hotelroom.GetHotelRooms(HotelID);

        }

        // GET: api/HotelRooms/5
        [HttpGet("{HotelID}/Rooms/{RoomNumber}")]
        public async Task<ActionResult<HotelRoom>> GetHotelRoom(int HotelID,int RoomNumber)
        {
            return await _hotelroom.GetHotelRoom(HotelID, RoomNumber);

        }

        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{HotelID}/Rooms/{RoomNumber}")]
        public async Task<IActionResult> PutHotelRoom(int HotelID,int RoomNumber, HotelRoomDTO hotelRoom)
        {
            if (HotelID != hotelRoom.HotelID && RoomNumber !=hotelRoom.RoomNumber)
            {
                return BadRequest();
            }

            var updateHotelRoom = await _hotelroom.UpdateHotelRoom(HotelID,RoomNumber, hotelRoom);

            return Ok(updateHotelRoom);
        }

        // POST: api/HotelRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("api/Hotels/{HotelID}/Rooms")]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(HotelRoom hotelRoom,int HotelID)
        {
            if (hotelRoom == null)
            {
                return Problem("the model is null or has no data ");
            }




            var NewHotelRoom = await _hotelroom.Create(hotelRoom,HotelID);
            return Ok(NewHotelRoom);
            
        }

        // DELETE: api/HotelRooms/5
        [HttpDelete("{HotelID}/Rooms/{RoomNumber}")]
        public async Task<IActionResult> DeleteHotelRoom(int HotelID,int RoomNumber)
        {
            await _hotelroom.Delete(HotelID,RoomNumber);
            return NoContent();
        }

        //private bool HotelRoomExists(int id)
        //{
        //    return (_context.HotelRoom?.Any(e => e.HotelID == id)).GetValueOrDefault();
        //}
    }
}
