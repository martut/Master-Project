using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BoookingAIOProject.Repository;
using BoookingAIOProject.Repository.DAL;
using BoookingAIOProject.Repository.Factories;

namespace BoookingAIOProject.Controllers
{
    public class ItemController : ApiController
    {
        readonly IBookingEFRepository _repository;
        readonly ItemFactory _itemFactory = new ItemFactory();

        public ItemController()
        {
            _repository = new BookingEFRepository(new BookingContext());
        }

        public ItemController(IBookingEFRepository repository)
        {
            _repository = repository;
        }

        public IHttpActionResult Get()
        {
            try
            {
                var items = _repository.GetItems();

                return Ok(items.ToList().Select(i => _itemFactory.CreateItem(i)));
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
                var item = _repository.GetItem(id);

                if (item != null)
                {
                    var result = _itemFactory.CreateItem(item);
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

        public IHttpActionResult Post([FromBody] DTO.Item item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest();
                }

                var newItem = _itemFactory.CreateItem(item);

                var result = _repository.InsertItem(newItem);
                if (result.Status == RepositoryActionStatus.Created)
                {
                    var createdItem = _itemFactory.CreateItem(result.Entity);
                    return Created<DTO.Item>(Request.RequestUri + "/" + result.Entity.Id.ToString(), createdItem);
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
                var item = _repository.DeleteItem(id);
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
            catch (Exception )
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Put(int id, [FromBody] DTO.Item item)
        {
            try
            {
                if (item == null)
                {
                    BadRequest();
                }

                var updateItem = _itemFactory.CreateItem(item);

                var result = _repository.UpdateItem(updateItem);
                if (result.Status == RepositoryActionStatus.Updated)
                {
                    var updatedItem = _itemFactory.CreateItem(result.Entity);

                    return Ok(updatedItem);
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
