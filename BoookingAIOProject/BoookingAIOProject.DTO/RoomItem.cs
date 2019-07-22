namespace BoookingAIOProject.DTO
{
    public class RoomItem
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int ItemId { get; set; }

        public Item Item { get; set; }  
    }
}