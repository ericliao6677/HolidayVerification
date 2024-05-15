using Holiday.API.Common.Extensions;
using Holiday.API.Domain.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Holiday.API.Infrastructures.ExceptionHandler
{
    public class ModelBindingFilter : Attribute, IActionFilter 
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var errors = context.ModelState
                .Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
                .ToDictionary(
                    x => string.Join('.', x.Key.Split('.')).ToCamelCase(),
                    x => x.Value.Errors.Select(e => e.ErrorMessage)
                 );

            context.Result = new BadRequestObjectResult(ResultResponseExtension.Exception.BadRequest(errors));          
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
