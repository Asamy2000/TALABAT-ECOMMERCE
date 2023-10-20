using E_commerce.Core.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace E_commerce.Helper
{
    public class CahedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSeconds;

        public CahedAttribute(int timeToLiveInSeconds)
        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }

        //context --> holds the endpoint info 
        //next delegate reference to the next Action filter or the action itSelf  
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //get cash Service
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            //get cacheKey 
            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            //get cacheResponse
            var cacheResponse = await cacheService.GetCachedResponseAsync(cacheKey);

            //not null so the Response cached before in the memory
            if (cacheResponse != null)
            {
                var contentResult = new ContentResult() 
                { 
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;
                //exist don't execute the endpoint
                return;
            }

            //Invoke the action if not cached
            var executedEndPointContext = await next.Invoke();

            if(executedEndPointContext.Result is OkObjectResult okObjectResult)
            {
                await cacheService.CacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveInSeconds));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var KeyBuilder = new StringBuilder();

            KeyBuilder.Append(request.Path); //Url Path

            foreach (var (key,value) in request.Query.OrderBy(Kvp => Kvp.Key))
            {
                KeyBuilder.Append($"|{key}-{value}");
                //path | Kvp
                //path | Kvp | Kvp
                //path | Kvp | Kvp  ...until the end of each Kvp in the Query string
            }
            return KeyBuilder.ToString();
        }
    }
}
