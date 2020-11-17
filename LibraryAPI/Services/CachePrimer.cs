using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAPI.Services
{
    public class CachePrimer : BackgroundService
    {
        private readonly ILogger<CachePrimer> _logger;
        private readonly IDistributedCache _cache;

        public CachePrimer(IDistributedCache cache, ILogger<CachePrimer> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public CachePrimer(ILogger<CachePrimer> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(10000);
                _logger.LogInformation("Another 10 seconds of my life have passed. time to make the donuts");
                var email = $"bob-{DateTime.Now.Millisecond}@aol.com";
                var encodedEmail = Encoding.UTF8.GetBytes(email);
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(15)
                };
                await _cache.SetAsync("oncall", encodedEmail, options);
            }
        }
    }
}
