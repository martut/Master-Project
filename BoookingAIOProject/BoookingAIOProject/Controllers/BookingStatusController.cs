using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BoookingAIOProject.DTO;
using BoookingAIOProject.Repository;
using BoookingAIOProject.Repository.DAL;
using BoookingAIOProject.Repository.Factories;

namespace BoookingAIOProject.Controllers
{
    public class BookingStatusController : ApiController
    {
        readonly IBookingEFRepository _repository;
        readonly BookingStatusFactory _statusFactory = new BookingStatusFactory();

        public BookingStatusController()
        {
            _repository = new BookingEFRepository(new BookingContext());
        }

        public BookingStatusController(IBookingEFRepository repository)
        {
            _repository = repository;
        }


        public IHttpActionResult Get()
        {
            try
            {

                var bookingStatuses = _repository.GetBookingStatuses();

                return Ok(bookingStatuses.ToList().Select(b => _statusFactory.CreateBookingStatus(b)));
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
                var bookingS = _repository.GetBookingStatus(id);

                if (bookingS != null)
                {
                    var result = _statusFactory.CreateBookingStatus(bookingS);
                    return Ok(result);
                }

                return BadRequest();
            }
            catch (Exception )
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Post([FromBody] DTO.BookingStatus bookingStatus)
        {
            try
            {
                if (bookingStatus == null)
                {
                    return BadRequest();
                }

                var BookingS = _statusFactory.CreateBookingStatus(bookingStatus);

                var result = _repository.InsertBookingStatus(BookingS);
                if (result.Status == RepositoryActionStatus.Created)
                {
                    var newBookingS = _statusFactory.CreateBookingStatus(result.Entity);
                    return Created<BookingStatus>(Request.RequestUri + "/" + newBookingS.Id.ToString(), newBookingS);
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Put(int id, [FromBody] DTO.BookingStatus bookingStatus)
        {
            try
            {
                if (bookingStatus == null)
                {
                    return BadRequest();
                }

                var updateBookingS = _statusFactory.CreateBookingStatus(bookingStatus);

                var result = _repository.UpdateBookingStatus(updateBookingS);
                if (result.Status == RepositoryActionStatus.Updated)
                {
                    var updatedBookingS = _statusFactory.CreateBookingStatus(result.Entity);
                    return Ok(updatedBookingS);
                }
                else if(result.Status == RepositoryActionStatus.NotFound)
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

        public IHttpActionResult Delete(int id)
        {
            try
            {
                var bookingStatus = _repository.DeleteBookingStatus(id);
                if (bookingStatus.Status == RepositoryActionStatus.Deleted)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else if (bookingStatus.Status == RepositoryActionStatus.NotFound)
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
