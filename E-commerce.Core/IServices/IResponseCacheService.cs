using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.IServices
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cachekey, object response , TimeSpan timeToLive);

        Task<string> GetCachedResponseAsync(string cachekey);
    }
}
