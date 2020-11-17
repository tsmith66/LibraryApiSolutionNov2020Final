using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Services
{
    public class RedisOnCallLookup : ILookupOnCallDevelopers
    {
        private readonly IDistributedCache _cache;

        public RedisOnCallLookup(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<string> GetOnCallDeveloperAsync()
        {
            // first, ask the cache (redis) if it has a good version of it.
            var emailFromCache = await _cache.GetAsync("oncall");
            string email = null;

            // if it does, just return that.
            if (emailFromCache != null)
            {
                var decodedString = Encoding.UTF8.GetString(emailFromCache);
                email = decodedString;
            }
            else
            {
                await Task.Delay(new Random().Next(1000, 3000)); // take one to three seconds to get it
                email = $"bob-{DateTime.Now.Millisecond}@aol.com";
                var encodedEmail = Encoding.UTF8.GetBytes(email);
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(15)
                };
                await _cache.SetAsync("oncall", encodedEmail, options);
            };
            return email;
        }
    }
}
