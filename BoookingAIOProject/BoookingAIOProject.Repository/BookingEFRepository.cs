using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoookingAIOProject.Repository.DAL;
using BoookingAIOProject.Repository.Entities;

namespace BoookingAIOProject.Repository
{
    public class BookingEFRepository : IBookingEFRepository
    {
        private readonly BookingContext _ctx;

        public BookingEFRepository(BookingContext ctx)
        {
            _ctx = ctx;


            _ctx.Configuration.LazyLoadingEnabled = false;
        }

        public Room GetRoom(int id)
        {
            return _ctx.Rooms.FirstOrDefault(r => r.Id == id);
        }

        public Room GetRoomByRoomNumber(int rNumber)
        {
            return _ctx.Rooms.FirstOrDefault(r => r.RoomNumber == rNumber);
        }

        public IQueryable<Room> GetRooms()
        {
            return _ctx.Rooms;
        }


        public RepositoryActionResult<Room> DeleteRoom(int id)
        {
            try
            {
                var room = _ctx.Rooms.FirstOrDefault(r => r.Id == id);

                if (room != null)
                {
                    _ctx.Rooms.Remove(room);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<Room>(null, RepositoryActionStatus.Deleted);
                }
                else
                {
                    return new RepositoryActionResult<Room>(null, RepositoryActionStatus.NotFound);
                }
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<Room>(null, RepositoryActionStatus.Error, exception);
            }
        }

        public RepositoryActionResult<Room> InsertRoom(Room r)
        {
            try
            {
                _ctx.Rooms.Add(r);
                var result = _ctx.SaveChanges();
                if (result > 0)
                    return new RepositoryActionResult<Room>(r, RepositoryActionStatus.Created);
                return new RepositoryActionResult<Room>(r, RepositoryActionStatus.NothingModified, null);
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<Room>(null, RepositoryActionStatus.Error, exception);
            }
        }

        public RepositoryActionResult<Room> UpdateRoom(Room r)
        {
            try
            {
                var existingRoom = _ctx.Rooms.FirstOrDefault(exg => exg.Id == r.Id);

                if (existingRoom == null)
                {
                    return new RepositoryActionResult<Room>(r, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingRoom).State = EntityState.Detached;
                _ctx.Rooms.Attach(r);
                _ctx.Entry(r).State = EntityState.Modified;

                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Room>(r, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Room>(r, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<Room>(null, RepositoryActionStatus.Error, exception);
            }
        }

        public Booking GetBooking(int id)
        {
            return _ctx.Bookings.FirstOrDefault(b => b.Id == id);
        }

        public Booking GetBookingWithRoom(int id)
        {
            return _ctx.Bookings.Include("Room").FirstOrDefault(b => b.Id == id);

        }

        public IQueryable<Booking> GetBookings()
        {
            return _ctx.Bookings;
        }

        public IQueryable<Booking> GetBookings(int roomId)
        {
            return _ctx.Bookings.Where(b => b.RoomId == roomId);
        }

        public IQueryable<Booking> GetBookingsWithRooms()
        {
            return _ctx.Bookings.Include("Room").Include("BookingStatus");
        }

        public RepositoryActionResult<Booking> DeleteBooking(int id)
        {
            try
            {
                var booking = _ctx.Bookings.FirstOrDefault(r => r.Id == id);

                if (booking != null)
                {
                    _ctx.Bookings.Remove(booking);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<Booking>(null, RepositoryActionStatus.Deleted);
                }
                else
                {
                    return new RepositoryActionResult<Booking>(null, RepositoryActionStatus.NotFound);
                }
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<Booking>(null, RepositoryActionStatus.Error, exception);
            }
        }

        public RepositoryActionResult<Booking> InsertBooking(Booking b)
        {
            try
            {
                _ctx.Bookings.Add(b);
                var result = _ctx.SaveChanges();
                if (result > 0)
                    return new RepositoryActionResult<Booking>(b, RepositoryActionStatus.Created);
                return new RepositoryActionResult<Booking>(b, RepositoryActionStatus.NothingModified, null);
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<Booking>(null, RepositoryActionStatus.Error, exception);
            }
        }

        public RepositoryActionResult<Booking> UpdateBooking(Booking b)
        {
            try
            {
                var existingBooking = _ctx.Bookings.FirstOrDefault(ex => ex.Id == b.Id);

                if (existingBooking == null)
                {
                    return new RepositoryActionResult<Booking>(b, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingBooking).State = EntityState.Detached;

                _ctx.Bookings.Attach(b);

                _ctx.Entry(b).State = EntityState.Modified;

                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Booking>(b, RepositoryActionStatus.Updated);

                }
                else
                {
                    return new RepositoryActionResult<Booking>(b, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<Booking>(null, RepositoryActionStatus.Error, exception);
            }
        }

        public BookingStatus GetBookingStatus(int id)
        {
            return _ctx.BookingStatuses.FirstOrDefault(b => b.Id == id);
        }

        public IQueryable<BookingStatus> GetBookingStatuses()
        {
            return _ctx.BookingStatuses;
        }

        public RoomItem GetRoomItem(int id)
        {
            return _ctx.RoomItems.FirstOrDefault(r => r.Id == id);
        }

        public IQueryable<RoomItem> GetRoomItems()
        {
            return _ctx.RoomItems;
        }

        public IQueryable<RoomItem> GetRoomItemsWithItems()
        {
            return _ctx.RoomItems.Include("Items");
        }

        public IQueryable<RoomItem> GetRoomItems(int roomId)
        {
            return _ctx.RoomItems.Where(r => r.RoomId == roomId);
        }

        public Item GetItem(int id)
        {
            return _ctx.Items.FirstOrDefault(i => i.Id == id);
        }

        public IQueryable<Item> GetItems()
        {
            return _ctx.Items;
        }

        public RoomService GetRoomService(int id)
        {
            return _ctx.RoomServices.FirstOrDefault(r => r.Id == id);
        }

        public IQueryable<RoomService> GetRoomServices()
        {
            return _ctx.RoomServices;
        }

        public IQueryable<RoomService> GetRoomServicesWithTypes()
        {
            return _ctx.RoomServices.Include("RoomServiceType");
        }

        public IQueryable<RoomService> GetRoomServicesWithTypes(int bookingId)
        {
            return _ctx.RoomServices
                .Include("RoomServiceType")
                .Where(r => r.BookingId == bookingId);
        }

        public IQueryable<RoomService> GetRoomServices(int bookingId)
        {
            return _ctx.RoomServices.Where(r => r.BookingId == bookingId);
        }

        public RoomServiceType GetRoomServiceType(int id)
        {
            return _ctx.RoomServiceTypes.FirstOrDefault(r => r.Id == id);
        }

        public IQueryable<RoomServiceType> GetRoomServiceTypes()
        {
            return _ctx.RoomServiceTypes;
        }

        public RepositoryActionResult<RoomItem> DeleteRoomItem(int id)
        {
            try
            {
                var roomItem = _ctx.RoomItems.FirstOrDefault(r => r.Id == id);

                if (roomItem != null)
                {
                    _ctx.RoomItems.Remove(roomItem);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<RoomItem>(null, RepositoryActionStatus.Deleted);
                }
                else
                {
                    return new RepositoryActionResult<RoomItem>(null, RepositoryActionStatus.NotFound);
                }
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<RoomItem>(null, RepositoryActionStatus.Error, exception);
            }
        }

        public RepositoryActionResult<RoomItem> InsertRoomItem(RoomItem ri)
        {
            try
            {

                _ctx.RoomItems.Add(ri);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<RoomItem>(ri, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<RoomItem>(null, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<RoomItem>(null, RepositoryActionStatus.Error, exception);
            }

        }

        public RepositoryActionResult<RoomItem> UpdateRoomItem(RoomItem ri)
        {
            try
            {
                var existingRi = _ctx.RoomItems.FirstOrDefault(exg => exg.Id == ri.Id);

                if (existingRi == null)
                {
                    return new RepositoryActionResult<RoomItem>(ri, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingRi).State = EntityState.Detached;
                _ctx.RoomItems.Attach(ri);
                _ctx.Entry(ri).State = EntityState.Modified;

                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<RoomItem>(ri, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<RoomItem>(ri, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<RoomItem>(null, RepositoryActionStatus.Error, exception);
            }
        }


        public RepositoryActionResult<Item> DeleteItem(int id)
        {
            try
            {
                var item = _ctx.Items.FirstOrDefault(r => r.Id == id);

                if (item != null)
                {
                    _ctx.Items.Remove(item);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<Item>(null, RepositoryActionStatus.Deleted);
                }
                else
                {
                    return new RepositoryActionResult<Item>(null, RepositoryActionStatus.NotFound);
                }
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<Item>(null, RepositoryActionStatus.Error, exception);
            }
        }

        public RepositoryActionResult<Item> InsertItem(Item i)
        {
            try
            {
                _ctx.Items.Add(i);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Item>(i, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Item>(null, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<Item>(null, RepositoryActionStatus.Error, exception);
            }

        }

        public RepositoryActionResult<Item> UpdateItem(Item i)
        {
            try
            {
                var existingI = _ctx.Items.FirstOrDefault(exg => exg.Id == i.Id);

                if (existingI == null)
                {
                    return new RepositoryActionResult<Item>(i, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingI).State = EntityState.Detached;
                _ctx.Items.Attach(i);
                _ctx.Entry(i).State = EntityState.Modified;

                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Item>(i, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Item>(i, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<Item>(null, RepositoryActionStatus.Error, exception);
            }

        }


        public RepositoryActionResult<RoomService> DeleteRoomService(int id)
        {
            try
            {
                var roomService = _ctx.RoomServices.FirstOrDefault(r => r.Id == id);

                if (roomService != null)
                {
                    _ctx.RoomServices.Remove(roomService);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<RoomService>(null, RepositoryActionStatus.Deleted);
                }
                else
                {
                    return new RepositoryActionResult<RoomService>(null, RepositoryActionStatus.NotFound);
                }
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<RoomService>(null, RepositoryActionStatus.Error, exception);

            }
        }

        public RepositoryActionResult<RoomService> InsertRoomService(RoomService rs)
        {
            try
            {
                _ctx.RoomServices.Add(rs);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<RoomService>(rs, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<RoomService>(null, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<RoomService>(null, RepositoryActionStatus.Error, exception);
            }
        }

        public RepositoryActionResult<RoomService> UpdateRoomService(RoomService rs)
        {
            try
            {
                var existingRs = _ctx.RoomServices.FirstOrDefault(exg => exg.Id == rs.Id);

                if (existingRs == null)
                {
                    return new RepositoryActionResult<RoomService>(rs, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingRs).State = EntityState.Detached;
                _ctx.RoomServices.Attach(rs);
                _ctx.Entry(rs).State = EntityState.Modified;

                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<RoomService>(rs, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<RoomService>(rs, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<RoomService>(null, RepositoryActionStatus.Error, exception);
            }


        }


        public RepositoryActionResult<RoomServiceType> DeleteRoomServiceType(int id)
        {
            try
            {
                var roomServiceType = _ctx.RoomServiceTypes.FirstOrDefault(r => r.Id == id);

                if (roomServiceType != null)
                {
                    _ctx.RoomServiceTypes.Remove(roomServiceType);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<RoomServiceType>(null, RepositoryActionStatus.Deleted);
                }
                else
                {
                    return new RepositoryActionResult<RoomServiceType>(null, RepositoryActionStatus.NotFound);
                }
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<RoomServiceType>(null, RepositoryActionStatus.Error, exception);

            }

        }

        public RepositoryActionResult<RoomServiceType> InsertRoomServiceType(RoomServiceType rst)
        {
            try
            {
                _ctx.RoomServiceTypes.Add(rst);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<RoomServiceType>(rst, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<RoomServiceType>(null, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<RoomServiceType>(null, RepositoryActionStatus.Error, exception);
            }
        }

        public RepositoryActionResult<RoomServiceType> UpdateRoomServiceType(RoomServiceType rst)
        {
            try
            {
                var existingRst = _ctx.RoomServiceTypes.FirstOrDefault(exg => exg.Id == rst.Id);

                if (existingRst == null)
                {
                    return new RepositoryActionResult<RoomServiceType>(rst, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingRst).State = EntityState.Detached;
                _ctx.RoomServiceTypes.Attach(rst);
                _ctx.Entry(rst).State = EntityState.Modified;

                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<RoomServiceType>(rst, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<RoomServiceType>(rst, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<RoomServiceType>(null, RepositoryActionStatus.Error, exception);
            }

        }

        public IQueryable<RoomItem> GetRoomItemsWithItems(int roomId)
        {
            return _ctx.RoomItems.Include("Item").Where(r => r.RoomId == roomId);
        }


        public RepositoryActionResult<BookingStatus> DeleteBookingStatus(int id)
        {
            try
            {
                var bs = _ctx.BookingStatuses.FirstOrDefault(r => r.Id == id);

                if (bs != null)
                {
                    _ctx.BookingStatuses.Remove(bs);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<BookingStatus>(null, RepositoryActionStatus.Deleted);
                }
                else
                {
                    return new RepositoryActionResult<BookingStatus>(null, RepositoryActionStatus.NotFound);
                }
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<BookingStatus>(null, RepositoryActionStatus.Error, exception);
            }
        }

        public RepositoryActionResult<BookingStatus> InsertBookingStatus(BookingStatus bs)
        {
            try
            {
                _ctx.BookingStatuses.Add(bs);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<BookingStatus>(bs, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<BookingStatus>(null, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<BookingStatus>(null, RepositoryActionStatus.Error, exception);
            }
        }

        public RepositoryActionResult<BookingStatus> UpdateBookingStatus(BookingStatus bs)
        {
            try
            {
                var existingBs = _ctx.BookingStatuses.FirstOrDefault(exg => exg.Id == bs.Id);

                if (existingBs == null)
                {
                    return new RepositoryActionResult<BookingStatus>(bs, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingBs).State = EntityState.Detached;
                _ctx.BookingStatuses.Attach(bs);
                _ctx.Entry(bs).State = EntityState.Modified;

                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<BookingStatus>(bs, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<BookingStatus>(bs, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<BookingStatus>(null, RepositoryActionStatus.Error, exception);
            }
        }
    }
}
