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
    public class RoomsController : ControllerBase
    {
        private readonly IRoom _room;

        public RoomsController(IRoom room)
        {
            _room = room;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
            return await _room.GetRooms();

        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            return await _room.GetRoom(id);
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            var updateRoom = await _room.UpdateRoom(id, room);

            return Ok(updateRoom);
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("({roomId}/Amenity/{amenityId}")]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            await _room.Create(room);

            // Rurtn a 201 Header to Browser or the postmane
            return CreatedAtAction("GetRoom", new { id = room.Id }, room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("({roomId}/Amenity/{amenityId}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _room.Delete(id);
            return NoContent();
        }

        //private bool RoomExists(int id)
        //{
        //    return (_context.Rooms?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
