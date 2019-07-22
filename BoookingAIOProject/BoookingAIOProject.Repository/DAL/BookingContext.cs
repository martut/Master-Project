using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoookingAIOProject.Repository.Entities;

namespace BoookingAIOProject.Repository.DAL
{
    public class BookingContext : DbContext
    {
        public BookingContext()
            : base("BookingContextCS")
        {
        }

        public virtual DbSet<BookingStatus> BookingStatuses { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<RoomItem> RoomItems { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<RoomService> RoomServices { get; set; }
        public virtual DbSet<RoomServiceType> RoomServiceTypes { get; set; }

    }
}
