using BoookingAIOProject.Repository.Entities;

namespace BoookingAIOProject.Repository.Factories
{
    public class ItemFactory
    {
        public Item CreateItem(DTO.Item item)
        {
            return new Item()
            {
                Id = item.Id,
                Name = item.Name
            };
        }

        public DTO.Item CreateItem(Item item)
        {
            return new DTO.Item()
            {
                Id = item.Id,
                Name = item.Name
            };
        }
    }
}