using System.ComponentModel.DataAnnotations;

namespace BoookingAIOProject.Repository.Entities
{
    public class RoomServiceType
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public double Price { get; set; }
        

    }
}