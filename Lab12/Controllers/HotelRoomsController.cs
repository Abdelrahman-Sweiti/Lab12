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
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetHotelRooms()
        {
            return await _hotelroom.GetHotelRooms();

        }

        // GET: api/HotelRooms/5
        [HttpGet("{HotelID}/Rooms/{RoomNumber}")]
        public async Task<ActionResult<HotelRoom>> GetHotelRoom(int HotelID,int RoomID)
        {
            return await _hotelroom.GetHotelRoom(HotelID,RoomID);

        }

        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{HotelID}/Rooms/{RoomNumber}")]
        public async Task<IActionResult> PutHotelRoom(int HotelID,int RoomID, HotelRoom hotelRoom)
        {
            if (HotelID != hotelRoom.HotelID && RoomID !=hotelRoom.RoomID)
            {
                return BadRequest();
            }

            var updateHotelRoom = await _hotelroom.UpdateHotelRoom(HotelID,RoomID, hotelRoom);

            return Ok(updateHotelRoom);
        }

        // POST: api/HotelRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{HotelID}/Rooms")]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(HotelRoom hotelRoom)
        {
            if (hotelRoom == null)
            {
                return Problem("the model is null or has no data ");
            }




            var NewHotelRoom = await _hotelroom.Create(hotelRoom);
            return Ok(NewHotelRoom);
            
        }

        // DELETE: api/HotelRooms/5
        [HttpDelete("{HotelID}/Rooms/{RoomNumber}")]
        public async Task<IActionResult> DeleteHotelRoom(int HotelID,int RoomID)
        {
            await _hotelroom.Delete(HotelID,RoomID);
            return NoContent();
        }

        //private bool HotelRoomExists(int id)
        //{
        //    return (_context.HotelRoom?.Any(e => e.HotelID == id)).GetValueOrDefault();
        //}
    }
}
