using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoookingAIOProject.Repository.Entities;

namespace BoookingAIOProject.Repository.Factories
{
    public class RoomFactory
    {
        public Room CreateRoom(DTO.Room room)
        {
            return new Room()
            {
                Id = room.Id,
                AdultsCapacity = room.AdultsCapacity,
                ChildrenCapacity = room.ChildrenCapacity,
                Price = room.Price,
                RoomNumber = room.RoomNumber
            };
        }


        public DTO.Room CreateRoom(Room room)
        {
            return new DTO.Room()
            {
                Id = room.Id,
                AdultsCapacity = room.AdultsCapacity,
                ChildrenCapacity = room.ChildrenCapacity,
                Price = room.Price,
                RoomNumber = room.RoomNumber
            };
        }
    }
}
