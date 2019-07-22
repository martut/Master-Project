using System.ComponentModel.DataAnnotations;
using System;

namespace BoookingAIOProject.Repository.Entities
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
    }
}