using Holiday.API.Domain.Response;
using Microsoft.Extensions.Caching.Memory;

namespace Holiday.API.Infrastructures.JWTToken
{
    public class CheckblacklistMiddleware
    {
        public readonly RequestDelegate _next;
        public readonly IMemoryCache _cache;

        public CheckblacklistMiddleware(RequestDelegate next, IMemoryCache cache) 
        {  
            _next = next; 
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Claims.Count() == 0)
            {
                await _next(context);
            }

            string jti = context.User.Claims.FirstOrDefault(p => p.Type == "jti").Value;
            if (_cache.TryGetValue(jti, out var cacheEntry))
            {
                await context.Response.WriteAsJsonAsync(
                    ResultResponseExtension.Verify.LoginVerificationError()
                );
            }

        }
    }
}
