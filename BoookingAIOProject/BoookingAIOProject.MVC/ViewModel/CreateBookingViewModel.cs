using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BoookingAIOProject.DTO;

namespace BoookingAIOProject.MVC.ViewModel
{
    public class CreateBookingViewModel
    {
        public Booking Booking { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ChInDateTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ChOutDateTime { get; set; }

        public IEnumerable<RoomItem> RoomItems { get; set; }

    }
}