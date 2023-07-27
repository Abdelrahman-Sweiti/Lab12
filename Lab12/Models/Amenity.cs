namespace Lab12.Models
{
    public class Amenity
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public List<RoomAmenity> RoomAmenities { get; set; }

    }
}
