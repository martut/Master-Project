using BoookingAIOProject.Repository.Entities;

namespace BoookingAIOProject.Repository.Factories
{
    public class RoomServiceTypeFactory
    {
        public RoomServiceType CreateRoomServiceType(DTO.RoomServiceType roomServiceType)
        {
            return new RoomServiceType()
            {
                Id = roomServiceType.Id,
                Name = roomServiceType.Name,
                IsActive = roomServiceType.IsActive,
                Price = roomServiceType.Price
            };
        }

        public DTO.RoomServiceType CreateRoomServiceType(RoomServiceType roomServiceType)
        {
            return new DTO.RoomServiceType()
            {
                Id = roomServiceType.Id,
                IsActive = roomServiceType.IsActive,
                Name = roomServiceType.Name,
                Price = roomServiceType.Price
            };
        }
    }
}