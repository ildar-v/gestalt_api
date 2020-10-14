using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Gestalt.Api.Models;
using Gestalt.Common.DAL.Entities;
using Gestalt.Common.DAL.MongoDBImpl;
using Microsoft.Extensions.Logging;

namespace Gestalt.Api.Services
{
    public class RequestsCache
    {
        private readonly IMongoRepository<RequestEntity> _requestRepository;
        private readonly ILogger<RequestsCache> _logger;

        public RequestsCache(IMongoRepository<RequestEntity> requestRepository, ILogger<RequestsCache> logger)
        {
            _requestRepository = requestRepository;
            _logger = logger;
        }

        private List<RequestEntity> _requestEntities;

        public async Task<List<RequestEntity>> RequestEntities()
        {
            if (_requestEntities != null)
            {
                return _requestEntities;
            }

            _logger.LogInformation("Starting caching requests...");
            var sw = Stopwatch.StartNew();
            _requestEntities = (await _requestRepository.FindAsync()).ToList();
            _logger.LogInformation($"Caching requests ended in {sw.Elapsed:G}");

            return _requestEntities;
        }

        public async Task<List<RequestEntity>> RequestEntities(RequestsFilter filter)
        {
            var requests = await RequestEntities();
            var requestNew = requests.Select(
                    x =>
                        new
                        {
                            id = x.RequestId,
                            longtitude = float.Parse(x._long),
                            latitude = float.Parse(x.lat)
                        })
                .Where(x =>
                    x.longtitude > filter.StartLongitude && x.latitude < filter.StartLatitude
                                                         && x.longtitude < filter.EndLongitude &&
                                                         x.latitude > filter.EndLatitude)
                .ToList();

            var requestNewIds = requestNew.Take(50).Select(x => x.id).ToList();

            var requestsInRequiredArea = requests
                .Where(x => requestNewIds.Contains(x.RequestId)).ToList();

            return requestsInRequiredArea.ToList();
        }
    }
}