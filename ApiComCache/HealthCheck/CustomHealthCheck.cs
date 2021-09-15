using ApiComCache.Cache;
using ApiComCache.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiComCache.HealthCheck
{
    public class CustomHealthCheck : IHealthCheck
    {

        private MemoryCache _cache;

        public CustomHealthCheck( MemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        private AtualizaCache RecuperarCache(String CacheKey)
        {

            AtualizaCache atualizaCache = new AtualizaCache();
            try
            {
                var resultado2 = _cache.Get(CacheKey);
                atualizaCache.RemontaCache = (bool)resultado2;
            }
            catch (Exception e)
            {
                atualizaCache.RemontaCache = false;
            }

            return atualizaCache;

        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {

            try
            {
                AtualizaCache atualizaCache = new AtualizaCache();
                atualizaCache = RecuperarCache("AtualizaMatriz");

                if (atualizaCache.RemontaCache)
                {
                    return Task.FromResult(HealthCheckResult.Unhealthy());
                }
                else
                {
                    return Task.FromResult(HealthCheckResult.Healthy());
                }

            }
            catch (Exception ex)
            {
                return Task.FromResult(new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex));
            }
        }

    }
}
