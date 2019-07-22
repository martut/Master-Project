using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BoookingAIOProject.DTO;
using BoookingAIOProject.Repository;
using BoookingAIOProject.Repository.DAL;
using BoookingAIOProject.Repository.Factories;

namespace BoookingAIOProject.Controllers
{
    public class RoomServiceController : ApiController
    {
        private IBookingEFRepository _repository;
        RoomServiceFactory _serviceFactory = new RoomServiceFactory();

        public RoomServiceController()
        {
            _repository = new BookingEFRepository(new BookingContext());
        }

        public RoomServiceController(IBookingEFRepository repository)
        {
            _repository = repository;
        }

        public IHttpActionResult Get()
        {
            try
            {
                var roomServ = _repository.GetRoomServices();

                var result = roomServ.ToList().Select(r => _serviceFactory.CreateRoomService(r));

                return Ok(result);
            }
            catch (Exception )
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                var roomServ = _repository.GetRoomService(id);
                if (roomServ != null)
                {
                    var result = _serviceFactory.CreateRoomService(roomServ);
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

        [Route("api/roomservice/booking/{id}")]
        public IHttpActionResult GetByBooking(int id)
        {
            try
            {
                var roomServices = _repository.GetRoomServicesWithTypes(id);

                var result = roomServices.ToList().Select(s => _serviceFactory.CreateRoomService(s));

                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Route("api/roomservices")]
        public IHttpActionResult GetRoomServicesWithTypes()
        {
            try
            {
                var roomServ = _repository.GetRoomServicesWithTypes();

                var result = roomServ.ToList().Select(r => _serviceFactory.CreateRoomService(r));

                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Post([FromBody] DTO.RoomService roomService)
        {
            try
            {
                if (roomService == null)
                {
                    return BadRequest();
                }

                var newRoomServ = _serviceFactory.CreateRoomService(roomService);
                var result = _repository.InsertRoomService(newRoomServ);
                if (result.Status == RepositoryActionStatus.Created)
                {
                    var createdRoomServ = _serviceFactory.CreateRoomService(result.Entity);
                    return Created<RoomService>(Request.RequestUri + "/" + createdRoomServ.Id.ToString(), createdRoomServ);
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
                var result = _repository.DeleteRoomService(id);
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

        public IHttpActionResult Put(int id, [FromBody] DTO.RoomService roomService)
        {
            try
            {
                if (roomService == null)
                {
                    return BadRequest();
                }

                var updateRoomServ = _serviceFactory.CreateRoomService(roomService);
                var result = _repository.UpdateRoomService(updateRoomServ);
                if (result.Status == RepositoryActionStatus.Updated)
                {
                    var updatedRoomServ = _serviceFactory.CreateRoomService(result.Entity);
                    return Ok(updatedRoomServ);
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
    }
}
