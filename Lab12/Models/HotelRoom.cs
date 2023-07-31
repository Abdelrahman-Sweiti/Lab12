using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab12.Models
{
    public class HotelRoom
    {
        
        public int HotelID { get; set; }
        public int RoomNumber { get; set; }
        [ForeignKey("Room")]
        public int RoomID { get; set; }
        public decimal Rate { get; set; }
        public bool PetFriendly { get; set; }


        public Hotel? Hotel { get; set; }
        public Room? Room { get; set; }

    }
}
