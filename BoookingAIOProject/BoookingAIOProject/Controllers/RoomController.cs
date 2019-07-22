using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BoookingAIOProject.Repository;
using BoookingAIOProject.Repository.DAL;
using BoookingAIOProject.Repository.Factories;
using Marvin.JsonPatch;

namespace BoookingAIOProject.Controllers
{
    public class RoomController : ApiController
    {
        readonly IBookingEFRepository _repository;
        readonly RoomFactory _roomFactory = new RoomFactory();

        public RoomController()
        {
            _repository = new BookingEFRepository(new BookingContext());
        }

        public RoomController(IBookingEFRepository repository)
        {
            _repository = repository;
        }


        public IHttpActionResult Get()
        {
            try
            {
                var rooms = _repository.GetRooms();

                return Ok(rooms.ToList().Select(r => _roomFactory.CreateRoom(r)));
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
                var room = _repository.GetRoom(id);

                if (room != null)
                {
                    var result = _roomFactory.CreateRoom(room);
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception )
            {
                return InternalServerError();
            }
        }

        [Route("api/room/number/{roomNumber}")]
        public IHttpActionResult GetByRoomNumber(int roomNumber)
        {
            try
            {
                var room = _repository.GetRoomByRoomNumber(roomNumber);

                if (room != null)
                {
                    var result = _roomFactory.CreateRoom(room);
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception )
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Post([FromBody] DTO.Room room)
        {
            try
            {
                if (room == null)
                {
                    return BadRequest();
                }

                var cRoom = _roomFactory.CreateRoom(room);

                var result = _repository.InsertRoom(cRoom);
                if (result.Status == RepositoryActionStatus.Created)
                {
                    var newRoom = _roomFactory.CreateRoom(result.Entity);
                    return Created<DTO.Room>(Request.RequestUri + "/" + newRoom.Id.ToString(), newRoom);
                }

                return BadRequest();
            }
            catch (Exception )
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _repository.DeleteRoom(id);

                if (result.Status == RepositoryActionStatus.Deleted)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else if (result.Status == RepositoryActionStatus.NotFound)
                {
                    return NotFound();
                }

                return BadRequest();
            }
            catch (Exception )
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Put(int id, [FromBody] DTO.Room room)
        {
            try
            {
                if (room == null)
                {
                    return BadRequest();
                }

                var updateRoom = _roomFactory.CreateRoom(room);
                var result = _repository.UpdateRoom(updateRoom);
                if (result.Status == RepositoryActionStatus.Updated)
                {
                    var updatedRoom = _roomFactory.CreateRoom(result.Entity);
                    return Ok(updatedRoom);
                }
                else if (result.Status == RepositoryActionStatus.NotFound)
                {
                    return NotFound();
                }

                return BadRequest();
            }
            catch (Exception )
            {
                return InternalServerError();
            }
        }

        [HttpPatch]
        public IHttpActionResult Patch(int id, [FromBody]JsonPatchDocument<DTO.Room> roomPatchDocument)
        {
            try
            {
                if (roomPatchDocument == null)
                {
                    return BadRequest();
                }

                var room = _repository.GetRoom(id);
                if (room == null)
                {
                    return NotFound();
                }

                var updateRoom = _roomFactory.CreateRoom(room);
                roomPatchDocument.ApplyTo(updateRoom);

                var result = _repository.UpdateRoom(_roomFactory.CreateRoom(updateRoom));
                if (result.Status == RepositoryActionStatus.Updated)
                {
                    var updatedRoom = _roomFactory.CreateRoom(result.Entity);
                    return Ok(updatedRoom);
                }

                return BadRequest();
            }
            catch (Exception )
            {
                return InternalServerError();
            }
        }


    }
}
