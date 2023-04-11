using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThucHanhDBFirst.Entites;

namespace ThucHanhDBFirst.Controllers
{
    public class ProductController : BaseController<ProductController>
    {
        public ProductController( DatabaseDemoContext demoContext, ILogger<ProductController> logger):base(demoContext, logger)
        {

        }

        [HttpGet]
        public IActionResult GetList()
        {
            var res = _dbContext.Products.ToList();
            return Ok(res);
        }

        [HttpGet]
        public IActionResult GetDetail(long id)
        {
            var res = _dbContext.Products.Find(id);
            return Ok(res);
        }

        [HttpPost]
        public IActionResult Create(Product obj)
        {
            _dbContext.Products.Add(obj);
            var s = _dbContext.SaveChanges();
            return s > 0 ? Ok("Create product success!") : BadRequest("Create failed");
        }

        [HttpPut]
        public IActionResult Update(Product obj)
        {
            _dbContext.Products.Update(obj);
            var s = _dbContext.SaveChanges();
            return s > 0 ? Ok("Update data success!") : BadRequest("Update failed");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var item = _dbContext.Products.Find(id);
            _dbContext.Products.Remove(item);
            if (item == null) return BadRequest("Not data found");
            return Ok("Delete data success!");
        }
    }
}
