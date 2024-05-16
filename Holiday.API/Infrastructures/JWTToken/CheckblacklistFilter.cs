using Holiday.API.Domain.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace Holiday.API.Infrastructures.JWTToken
{
    public class CheckblacklistFilter : IAsyncAuthorizationFilter
    {
        public readonly IMemoryCache _cache;

        public CheckblacklistFilter(IMemoryCache cache)
        {
            _cache = cache;
        }
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Claims.Count() != 0)
            {
                var jti = context.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "jti").Value;

                if (_cache.TryGetValue(jti, out string cacheEntry))
                {
                    context.Result = new ObjectResult(ResultResponseExtension.Verify.TokenError());
                }
            }
            return Task.CompletedTask;
        }
    }
}
