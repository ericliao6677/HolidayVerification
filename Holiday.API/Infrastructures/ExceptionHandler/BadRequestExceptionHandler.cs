using Holiday.API.Common.Extensions;
using Holiday.API.Domain.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Holiday.API.Infrastructures.ExceptionHandler
{
    public static class BadRequestExceptionHandler
    {
        public static BadRequestObjectResult TryHandler(ActionContext context)
        {
            var errors = context.ModelState
                .Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
                .ToDictionary(
                    x => string.Join('.', x.Key.Split('.')).ToCamelCase(),
                    x => x.Value.Errors.Select(e => e.ErrorMessage)
                 );
            return new BadRequestObjectResult(ResultResponseExtension.Exception.BadRequest(errors));
        }

    }
}
