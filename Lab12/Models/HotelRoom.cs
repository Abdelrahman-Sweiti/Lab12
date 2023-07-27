using System.ComponentModel.DataAnnotations;

namespace Lab12.Models
{
    public class HotelRoom
    {
        
        public int HotelID { get; set; }
        public int RoomNumber { get; set; }
        public int RoomID { get; set; }
        public decimal Rate { get; set; }
        public bool PetFriendly { get; set; }


        public Hotel hotel { get; set; }
        public Room room { get; set; }

    }
}
