using ApiComCache.Cache;
using ApiComCache.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiComCache.Controllers
{
    [ApiController]
    [Route("api")]
    public class Controller : ControllerBase
    {
        private MemoryCache _cache;

        private readonly ILogger<Controller> _logger;

        public Controller(ILogger<Controller> logger, MyMemoryCache memoryCache)
        {
            _logger = logger;
            _cache = memoryCache.Cache;
        }

        [HttpGet]
        public async Task<Produto> Get()
        {
            AtualizaCache atualizaCache = new AtualizaCache();
            atualizaCache = RecuperarCache("AtualizaMatriz");
            Produto produto = new Produto();
            produto.AtualizaCache = atualizaCache.RemontaCache;

            return produto;

        }


        [HttpPost]
        public void Post([FromBody] AtualizaCache atualizaCache)
        {
            AlterarCache("AtualizaMatriz", true);

            Thread.Sleep(15000);

            AlterarCache("AtualizaMatriz", false);
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

            }

            return atualizaCache;

        }

        private string AlterarCache(String CacheKey, Boolean ItemValue)
        {
            String resultado = "";

            try
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                               .SetSize(1);
                _cache.Set(CacheKey, ItemValue, cacheEntryOptions);

            }
            catch (Exception e)
            {

            }

            return resultado;

        }

    }


}
