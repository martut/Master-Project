using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using BoookingAIOProject.Repository.Entities;

namespace BoookingAIOProject.Repository.Factories
{
    public class RoomItemFactory
    {
        ItemFactory itemFactory = new ItemFactory();
        public RoomItem CreateRoomItem(DTO.RoomItem roomItem)
        {
            return new RoomItem()
            {
                Id = roomItem.Id,
                ItemId = roomItem.ItemId,
                RoomId = roomItem.RoomId,
                Item = roomItem.Item == null ? null : itemFactory.CreateItem(roomItem.Item)
            };
        }

        public DTO.RoomItem CreateRoomItem(RoomItem roomItem)
        {
            return new DTO.RoomItem()
            {
                Id = roomItem.Id,
                RoomId = roomItem.RoomId,
                ItemId = roomItem.ItemId,
                Item = roomItem.Item == null ? null : itemFactory.CreateItem(roomItem.Item)
            };
        }




        //public object CreateRoomItemWithItem(DTO.RoomItem roomItem)
        //{
        //    ExpandoObject objectToReturn = new ExpandoObject();
        //    ExpandoObject itemObject = new ExpandoObject();

        //    ((IDictionary<string, object>)itemObject).Add("id", roomItem.Item);
        //    ((IDictionary<string, object>)itemObject).Add("roomId", roomItem.ItemId);

        //    ((IDictionary<string, object>)objectToReturn).Add("id",roomItem.Id);
        //    ((IDictionary<string, object>)objectToReturn).Add("roomId", roomItem.ItemId);
        //    ((IDictionary<string, object>)objectToReturn).Add("itemId", roomItem.RoomId);
        //    ((IDictionary<string, object>)objectToReturn).Add("item", itemObject);

        //}

        //public object CreateRoomItemWithItem(RoomItem roomItem)
        //{
        //    return CreateRoomItemWithItem(CreateRoomItem(roomItem));


        //}
    }
}