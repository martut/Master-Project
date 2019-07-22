using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoookingAIOProject.Repository.Entities;

namespace BoookingAIOProject.Repository
{
    public interface IBookingEFRepository
    {
        RepositoryActionResult<Booking> DeleteBooking(int id);
        RepositoryActionResult<Room> DeleteRoom(int id);
        RepositoryActionResult<BookingStatus> DeleteBookingStatus(int id);
        RepositoryActionResult<Booking> InsertBooking(Booking b);
        RepositoryActionResult<Room> InsertRoom(Room r);
        RepositoryActionResult<BookingStatus> InsertBookingStatus(BookingStatus bs);
        RepositoryActionResult<Booking> UpdateBooking(Booking b);
        RepositoryActionResult<Room> UpdateRoom(Room r);
        RepositoryActionResult<BookingStatus> UpdateBookingStatus(BookingStatus bs);
        Booking GetBooking(int id);
        Booking GetBookingWithRoom(int id);
        Room GetRoom(int id);
        Room GetRoomByRoomNumber(int rNumber);
        BookingStatus GetBookingStatus(int id);
        IQueryable<Booking> GetBookings();
        IQueryable<Booking> GetBookingsWithRooms();
        IQueryable<Booking> GetBookings(int roomId);
        IQueryable<Room> GetRooms();
        IQueryable<BookingStatus> GetBookingStatuses();

        RoomItem GetRoomItem(int id);
        IQueryable<RoomItem> GetRoomItems();
        IQueryable<RoomItem> GetRoomItemsWithItems();
        IQueryable<RoomItem> GetRoomItems(int roomId);
        Item GetItem(int id);   
        IQueryable<Item> GetItems();
        RoomService GetRoomService(int id);
        IQueryable<RoomService> GetRoomServices();
        IQueryable<RoomService> GetRoomServices(int bookingId);
        IQueryable<RoomService> GetRoomServicesWithTypes();
        IQueryable<RoomService> GetRoomServicesWithTypes(int bookingId);
        RoomServiceType GetRoomServiceType(int id);
        IQueryable<RoomServiceType> GetRoomServiceTypes();
        RepositoryActionResult<RoomItem> DeleteRoomItem(int id);
        RepositoryActionResult<RoomItem> InsertRoomItem(RoomItem ri);
        RepositoryActionResult<RoomItem> UpdateRoomItem(RoomItem ri);
        RepositoryActionResult<Item> DeleteItem(int id);
        RepositoryActionResult<Item> InsertItem(Item i);
        RepositoryActionResult<Item> UpdateItem(Item i);
        RepositoryActionResult<RoomService> DeleteRoomService(int id);
        RepositoryActionResult<RoomService> InsertRoomService(RoomService rs);
        RepositoryActionResult<RoomService> UpdateRoomService(RoomService rs);
        RepositoryActionResult<RoomServiceType> DeleteRoomServiceType(int id);
        RepositoryActionResult<RoomServiceType> InsertRoomServiceType(RoomServiceType rst);
        RepositoryActionResult<RoomServiceType> UpdateRoomServiceType(RoomServiceType rst);

        IQueryable<RoomItem> GetRoomItemsWithItems(int roomId);

    }
}
