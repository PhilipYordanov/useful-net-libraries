namespace MediatRFluentDemo.PipelineBehaviours
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Abstractions;
    using Settings;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;

    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger _logger;
        private readonly CacheSettings _settings;

        public CachingBehavior(IDistributedCache cache, ILogger<TResponse> logger, IOptions<CacheSettings> settings)
        {
            _cache = cache;
            _logger = logger;
            _settings = settings.Value;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response;
            if (!(typeof(ICacheableQuery).IsAssignableFrom(request.GetType())))
            {
                return await next();
            }

           var cacheableRequest = request as ICacheableQuery;

            // If the BypassCache is true, we go to the next request/response and avoids caching.
            if (cacheableRequest.BypassCache)
            {
                return await next();
            }

            // fetches from IDistributedCache instance and checks if any data with the passed cache key exists
            var cachedResponse = await _cache.GetAsync((string)cacheableRequest?.CacheKey, cancellationToken);
            string message = string.Empty;
            if (cachedResponse != null)
            {
                response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cachedResponse));
                message = $"Fetched from Cache";
            }
            else
            {
                response = await AddResponseToCache(next, cacheableRequest, cancellationToken);
                message = $"Added to Cache";
            }

            _logger.LogInformation($"{message} -> '{cacheableRequest.CacheKey}'.");
            return response;
        }

        private async Task<TResponse> AddResponseToCache(
            RequestHandlerDelegate<TResponse> next,
            ICacheableQuery request,
            CancellationToken cancellationToken)
        {
            TResponse response;

            //  waits for the response from the server/database.
            response = await next();

            // creates an expiration timespan in hours,
            // either from the appsettings or the value passed from the mediatR handler.
            // in this case each mediatR handler can decide how long a cache has to be available in the cache store.
            TimeSpan? expiration = default;
            if (request.Expiration == null)
            {
                expiration = TimeSpan.FromHours(_settings.Expiration);
            }
            else
            {
                expiration = request.Expiration;
            }

            var options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = expiration
            };

            var serializedData = Encoding.Default.GetBytes(JsonConvert.SerializeObject(response));

            // adds the serialized data against the cache key in the cache-store along with the cache options that we have built.
            await _cache.SetAsync((string)request.CacheKey, serializedData, options, cancellationToken);

            return response;
        }
    }
}
