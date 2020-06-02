using Microsoft.AspNetCore.Mvc;

namespace Gestalt.Api.Controllers
{
    using System.Threading.Tasks;
    using Gestalt.Common.DAL.Entities;
    using Gestalt.Common.DAL.MongoDBImpl;
    using Microsoft.AspNetCore.Authorization;

    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : Controller
    {
        private readonly IMongoRepository<MainResponseEntity> _mainResponseRepository;

        public RequestController(IMongoRepository<MainResponseEntity> mainResponseRepository)
        {
            _mainResponseRepository = mainResponseRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var requests = await _mainResponseRepository.FindAsync();
            return Ok(requests);
        }
    }
}