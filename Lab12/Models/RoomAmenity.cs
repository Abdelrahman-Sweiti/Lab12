namespace Lab12.Models
{
    public class RoomAmenity
    {
        public int AmenityID { get; set; }
        public int RoomID { get; set; }


        /// Nav Props
        /// 

        public Amenity? Amenity { get; set; }
        public Room? Room { get; set; }


    }
}
