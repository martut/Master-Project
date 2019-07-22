using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoookingAIOProject.DTO;

namespace BoookingAIOProject.MVC.ViewModel
{
    public class RoomEditViewModel
    {
        public Room Room { get; set; }
        public IEnumerable<RoomItem> RoomItems { get; set; }

        public int SelectedItemId { get; set; }
        public IEnumerable<SelectListItem> Items { get; set; }

    }
}