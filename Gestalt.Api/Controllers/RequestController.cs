using System.Threading.Tasks;
using Gestalt.Api.Models;
using Gestalt.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestalt.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : Controller
    {
        private readonly RequestsCache _requestsCache;

        public RequestController(RequestsCache requestsCache)
        {
            _requestsCache = requestsCache;
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            float startLatitude,
            float startLongitude,
            float endLatitude,
            float endLongitude)
        {
            var requestsFilter = new RequestsFilter
            {
                StartLatitude = startLatitude,
                StartLongitude = startLongitude,
                EndLatitude = endLatitude,
                EndLongitude = endLongitude
            };

            var result = await _requestsCache.RequestEntities(requestsFilter);

            return Ok(result);
        }
    }
}