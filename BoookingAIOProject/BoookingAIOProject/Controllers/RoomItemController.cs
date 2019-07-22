using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BoookingAIOProject.Repository;
using BoookingAIOProject.Repository.DAL;
using BoookingAIOProject.Repository.Entities;
using BoookingAIOProject.Repository.Factories;

namespace BoookingAIOProject.Controllers
{
    public class RoomItemController : ApiController
    {
        private IBookingEFRepository _repository;
        RoomItemFactory _roomItemFactory = new RoomItemFactory();

        public RoomItemController()
        {
            _repository = new BookingEFRepository(new BookingContext());
        }

        public RoomItemController(IBookingEFRepository repository)
        {
            _repository = repository;
        }

        public IHttpActionResult Get()
        {
            try
            {
                var roomItems = _repository.GetRoomItems();

                return Ok(roomItems.ToList().Select(i => _roomItemFactory.CreateRoomItem(i)));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                var roomItem = _repository.GetRoomItem(id);

                if (roomItem != null)
                {
                    var result = _roomItemFactory.CreateRoomItem(roomItem);
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Route("api/roomitem/items/{id}")]
        public IHttpActionResult GetWithItems(int id)
        {
            try
            {
                var roomitems = _repository.GetRoomItemsWithItems(id);

                var result = roomitems.ToList().Select(r => _roomItemFactory.CreateRoomItem(r));

                return Ok(result);
            }
            catch (Exception )
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Post([FromBody] DTO.RoomItem roomItem)
        {
            try
            {
                if (roomItem == null)
                {
                    return BadRequest();
                }

                var newItem = _roomItemFactory.CreateRoomItem(roomItem);

                var result = _repository.InsertRoomItem(newItem);
                if (result.Status == RepositoryActionStatus.Created)
                {
                    var createdItem = _roomItemFactory.CreateRoomItem(result.Entity);
                    return Created<DTO.RoomItem>(Request.RequestUri + "/" + result.Entity.Id.ToString(), createdItem);
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                var item = _repository.DeleteRoomItem(id);
                if (item.Status == RepositoryActionStatus.Deleted)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else if (item.Status == RepositoryActionStatus.NotFound)
                {
                    return NotFound();
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Put(int id, [FromBody] DTO.RoomItem item)
        {
            try
            {
                if (item == null)
                {
                    BadRequest();
                }

                var updateItem = _roomItemFactory.CreateRoomItem(item);

                var result = _repository.UpdateRoomItem(updateItem);
                if (result.Status == RepositoryActionStatus.Updated)
                {
                    var updatedItem = _roomItemFactory.CreateRoomItem(result.Entity);

                    return Ok(updatedItem);
                }
                else if (result.Status == RepositoryActionStatus.NotFound)
                {
                    return NotFound();
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

    }
}
