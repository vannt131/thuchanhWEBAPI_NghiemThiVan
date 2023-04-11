using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThucHanhDBFirst.Entites;

namespace ThucHanhDBFirst.Controllers
{
    public class CustomerController : BaseController<CustomerController>
    {
        public CustomerController(DatabaseDemoContext demoContext, ILogger<CustomerController> logger) : 
            base( demoContext, logger)
        { 
        
        }

        [HttpGet]
        public IActionResult GetList([FromQuery] Customer model)
        {
            var res = _dbContext.Customers.Where(m
                => (m.Name.ToLower().Contains(model.Name.ToLower())|| model.Name == "")
                   &&(m.Gender.ToLower().Contains(model.Gender.ToLower())|| model.Gender == "")
                   &&(m.Address.ToLower().Contains(model.Address.ToLower()) || model.Address == ""));
            return Ok(res);
        }

        [HttpGet]
        public IActionResult GetDetail(long id) 
        {
            var res = _dbContext.Customers.Find(id);
            var data = from ord in _dbContext.Orders
                       join pr in _dbContext.Products on ord.ProductId equals pr.Id
                       where ord.CustomerId == id
                       select new Product
                       {
                           Id = pr.Id,
                           Name = pr.Name,
                           Description = pr.Description,
                           Amount = ord.Amount,
                           Price = ord.Price
                       };

            return Ok(new CustomerOrder
            {
                Id = id,
                Name = res.Name,
                Age = res.Age,
                Gender = res.Gender,
                Address = res.Address,
                Description = res.Description,
                Username = res.Username,
                Status = res.Status,
                CreatedDate = res.CreatedDate,
                CreatedBy = res.CreatedBy,
                UpdatedBy = res.UpdatedBy,
                UpdatedDate = res.UpdatedDate,
                Products = data.ToList() 
            });
        }

        [HttpPost]
        public IActionResult Create(Customer obj)
        {
            _dbContext.Customers.Add(obj);
            var s = _dbContext.SaveChanges();
            return s > 0 ? Ok("Create customer success!"): BadRequest("Create failed");
        }

        [HttpPut]
        public IActionResult Update(Customer obj)
        {
            _dbContext.Customers.Update(obj);
            var s = _dbContext.SaveChanges();
            return s > 0 ? Ok("Update data success!"):BadRequest("Update failed");   
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var item = _dbContext.Customers.Find(id);
            _dbContext.Customers.Remove(item);
            if (item == null) return BadRequest("Not data found");
            return Ok("Delete data success!");
        }

        [HttpPost]
        public IActionResult OrderProduct(Order obj) 
        {
            var checkProduct = _dbContext.Products.Find(obj.ProductId);
            if (checkProduct == null) return BadRequest("No data product found");

            var checkCustomer = _dbContext.Customers.Find(obj.CustomerId);
            if (checkCustomer == null) return BadRequest("No data customer found");

            var checkOrderExist = _dbContext.Orders.Find(obj.ProductId, obj.CustomerId);
            if (checkOrderExist != null) return BadRequest("Order exist");

            _dbContext.Orders.Add(obj);
            var s = _dbContext.SaveChanges();
            return s > 0 ? Ok("Create order success") : BadRequest("Create failed");
        }
    }
}
