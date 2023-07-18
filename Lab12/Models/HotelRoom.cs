﻿using System.ComponentModel.DataAnnotations;

namespace Lab12.Models
{
    public class HotelRoom
    {
        
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int RoomID { get; set; }
        public decimal Rate { get; set; }
        public bool PetFriendly { get; set; }


    }
}
