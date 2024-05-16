using Holiday.API.Common.Extensions;
using Holiday.API.Domain.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Holiday.API.Infrastructures.ExceptionHandler
{
    public class ModelBindingFilter : IActionFilter 
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
                      
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                .Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
                .ToDictionary(
                    x => string.Join('.', x.Key.Split('.')).ToCamelCase(),
                    x => x.Value.Errors.Select(e => e.ErrorMessage)
                 );

                context.Result = new BadRequestObjectResult(ResultResponseExtension.Exception.BadRequest(errors));
            }
        }
    }
}
