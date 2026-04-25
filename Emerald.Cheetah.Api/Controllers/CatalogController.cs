using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Emerald.Cheetah.Domain.Catalog;
using Emerald.Cheetah.Data;
using Microsoft.AspNetCore.Authorization;

namespace Emerald.Cheetah.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {   private readonly StoreContext _db;

        public CatalogController(StoreContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetItems()
        {
            return Ok(_db.Items.Include(i => i.Ratings));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetItem(int id)
        {
            var item = _db.Items.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Post(Item item)
        {
            _db.Items.Add(item);
            _db.SaveChanges();
            return Created($"/catalog/{item.Id}", item);
        }

        [HttpPost("{id:int}/ratings")]
        public IActionResult PostRating(int id, [FromBody] Rating rating)
        {
            var item = _db.Items.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            item.AddRating(rating);
            _db.SaveChanges();

            return Ok(item);
        }

        [HttpPut("{id:int}")]
        public IActionResult PutItem(int id, [FromBody] Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            var existingItem = _db.Items.Find(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = item.Name;
            existingItem.Description = item.Description;
            existingItem.Brand = item.Brand;
            existingItem.Price = item.Price;

            _db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize("delete:catalog")]
        public IActionResult DeleteItem(int id)
        {
            var item = _db.Items.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            _db.Items.Remove(item);
            _db.SaveChanges();

            return NoContent();
        }

    }

}
