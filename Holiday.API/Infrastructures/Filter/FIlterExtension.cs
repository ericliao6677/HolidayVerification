using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Holiday.API.Infrastructures.ExceptionHandler;
using Holiday.API.Infrastructures.JWTToken;

namespace Holiday.API.Infrastructures.Filter
{
    public static class FIlterExtension
    {
        public static void AddFilter(this MvcOptions options)
        {
            options.Filters.Add<ModelBindingFilter>();
            options.Filters.Add<CheckblacklistFilter>();
            
        }
    }
}
