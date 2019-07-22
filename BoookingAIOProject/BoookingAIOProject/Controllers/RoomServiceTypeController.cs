using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Web.Http;
using BoookingAIOProject.DTO;
using BoookingAIOProject.Repository;
using BoookingAIOProject.Repository.DAL;
using BoookingAIOProject.Repository.Factories;

namespace BoookingAIOProject.Controllers
{
    public class RoomServiceTypeController : ApiController
    {
        private IBookingEFRepository _repository;
        RoomServiceTypeFactory _serviceTypeFactory = new RoomServiceTypeFactory();

        public RoomServiceTypeController()
        {
            _repository = new BookingEFRepository(new BookingContext());
        }

        public RoomServiceTypeController(IBookingEFRepository repository)
        {
            _repository = repository;
        }

        public IHttpActionResult Get()
        {
            try
            {
                var roomServType = _repository.GetRoomServiceTypes();
                var result = roomServType.ToList().Select(r => _serviceTypeFactory.CreateRoomServiceType(r));
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
                var roomServType = _repository.GetRoomServiceType(id);
                if (roomServType != null)
                {
                    var result = _serviceTypeFactory.CreateRoomServiceType(roomServType);
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

        public IHttpActionResult Post([FromBody] DTO.RoomServiceType roomServiceType)
        {
            try
            {
                if (roomServiceType == null)
                {
                    return BadRequest();
                }

                var newRoomServType = _serviceTypeFactory.CreateRoomServiceType(roomServiceType);
                var result = _repository.InsertRoomServiceType(newRoomServType);
                if (result.Status == RepositoryActionStatus.Created)
                {
                    var createdRoomServType = _serviceTypeFactory.CreateRoomServiceType(result.Entity);
                    return Created<RoomServiceType>(Request.RequestUri + "/" + createdRoomServType.Id.ToString(), createdRoomServType);
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
                var result = _repository.DeleteRoomServiceType(id);
                if (result.Status == RepositoryActionStatus.Deleted)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else if (result.Status== RepositoryActionStatus.NotFound)
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

        public IHttpActionResult PutActionResult(int id, [FromBody] DTO.RoomServiceType roomServiceType)
        {
            try
            {
                if (roomServiceType == null)
                {
                    return BadRequest();
                }

                var updateRoomServType = _serviceTypeFactory.CreateRoomServiceType(roomServiceType);
                var result = _repository.UpdateRoomServiceType(updateRoomServType);
                if (result.Status == RepositoryActionStatus.Updated)
                {
                    var updatedRoomServType = _serviceTypeFactory.CreateRoomServiceType(result.Entity);
                    return Ok(updatedRoomServType);
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
