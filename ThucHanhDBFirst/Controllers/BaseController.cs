using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThucHanhDBFirst.Entites;

namespace ThucHanhDBFirst.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        protected readonly DatabaseDemoContext _dbContext;
        protected readonly ILogger<T> _logger;
        public BaseController(DatabaseDemoContext dbContext, ILogger<T> logger) 
        { 
            _dbContext = dbContext;
            _logger = logger;
        }
    }
}
