namespace MediatRFluentDemo.Abstractions
{
    using System;

    public interface ICacheableQuery
    {
        bool BypassCache { get; }

        string CacheKey { get; }

        TimeSpan? Expiration { get; }
    }
}
