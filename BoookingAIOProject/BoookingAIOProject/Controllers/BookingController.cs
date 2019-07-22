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
    public class BookingController : ApiController
    {
        readonly IBookingEFRepository _repository;
        readonly BookingFactory _bookingFactory = new BookingFactory();

        public BookingController()
        {
            _repository = new BookingEFRepository(new BookingContext());
        }

        public BookingController(IBookingEFRepository repository)
        {
            _repository = repository;
        }


        public IHttpActionResult Get()
        {
            try
            {
                var bookings = _repository.GetBookings();

                return Ok(bookings.ToList().Select(b => _bookingFactory.CreateBooking(b)));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("api/bookings")]
        [HttpGet]
        public IHttpActionResult GeBookings()
        {
            try
            {
                var bookings = _repository.GetBookingsWithRooms();

                var result = bookings.ToList().Select(b => _bookingFactory.CreateBooking(b));

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
                var booking = _repository.GetBooking(id);

                if (booking != null)
                {
                    var result = _bookingFactory.CreateBooking(booking);
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
        [Route("api/bookingwithroom/{id}")]
        public IHttpActionResult GetBookingWithRoom(int id)
        {
            try
            {
                var booking = _repository.GetBookingWithRoom(id);

                if (booking != null)
                {
                    var result = _bookingFactory.CreateBooking(booking);
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


        public IHttpActionResult Post([FromBody] DTO.Booking booking)
        {
            try
            {
                if (booking == null)
                {
                    return BadRequest();
                }

                var mapBooking = _bookingFactory.CreateBooking(booking);

                var newBooking = _repository.InsertBooking(mapBooking);
                if (newBooking.Status == RepositoryActionStatus.Created)
                {
                    var result = _bookingFactory.CreateBooking(newBooking.Entity);
                    return Created<Booking>(Request.RequestUri + "/" + result.Id.ToString(), result);
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
                var deleteBooking = _repository.DeleteBooking(id);
                if (deleteBooking.Status == RepositoryActionStatus.Deleted)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else if (deleteBooking.Status == RepositoryActionStatus.NotFound)
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

        public IHttpActionResult Put(int id, [FromBody] DTO.Booking booking)
        {
            try
            {
                if (booking == null)
                {
                    return BadRequest();
                }

                var mapBooking = _bookingFactory.CreateBooking(booking);

                var updateBooking = _repository.UpdateBooking(mapBooking);
                if (updateBooking.Status == RepositoryActionStatus.Updated)
                {
                    var result = _bookingFactory.CreateBooking(updateBooking.Entity);
                    return Ok(result);
                }
                else if (updateBooking.Status == RepositoryActionStatus.NotFound)
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
