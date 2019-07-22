using BoookingAIOProject.Repository.Entities;

namespace BoookingAIOProject.Repository.Factories
{
    public class RoomServiceFactory
    {
        RoomServiceTypeFactory roomServTypeFactory = new RoomServiceTypeFactory();
        public RoomService CreateRoomService(DTO.RoomService roomService)
        {
            return new RoomService()
            {
                Id = roomService.Id,
                Amount = roomService.Amount,
                BookingId = roomService.BookingId,
                Date = roomService.Date,
                RoomServiceTypeId = roomService.RoomServiceTypeId,
                RoomServiceType = roomService.RoomServiceType == null ? null : roomServTypeFactory
                                                                                    .CreateRoomServiceType(roomService.RoomServiceType)
            };
        }

        public DTO.RoomService CreateRoomService(RoomService roomService)
        {
            return new DTO.RoomService()
            {
                Id = roomService.Id,
                Date = roomService.Date,
                BookingId = roomService.BookingId,
                RoomServiceTypeId = roomService.RoomServiceTypeId,
                Amount = roomService.Amount,
                RoomServiceType = roomService.RoomServiceType == null ? null : roomServTypeFactory
                                                                                    .CreateRoomServiceType(roomService.RoomServiceType)
            };
        }
    }
}