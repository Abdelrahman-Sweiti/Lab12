namespace Lab12.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Layout { get; set; }



        //Props
        public List<RoomAmenity>? RoomAmenities { get; set; }
        public List<HotelRoom>? HotelRoom { get; set; }


    }
}
