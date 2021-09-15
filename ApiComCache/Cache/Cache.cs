﻿using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiComCache.Cache
{
    // using Microsoft.Extensions.Caching.Memory;
    public class MyMemoryCache
    {
        public MemoryCache Cache { get; set; }
        public MyMemoryCache()
        {
            Cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 1024
            });
        }
    }
}
