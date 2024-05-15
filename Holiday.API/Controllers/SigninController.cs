using Azure.Core;
using Holiday.API.Domain.Request.Post;
using Holiday.API.Domain.Response;
using Holiday.API.Infrastructures.JWTToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Holiday.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SigninController : ControllerBase
    {
        private readonly JWTTokenHelper _jWTTokenHelper;
        public SigninController(JWTTokenHelper jWTTokenHelper)
        {
            _jWTTokenHelper = jWTTokenHelper;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Signin([FromBody] PostSigninRequest request)
        {
            if (request is not { Account: "Admin", Password: "=-09poiu" }) return Ok(ResultResponseExtension.Verify.LoginVerificationError());
            return Ok(ResultResponseExtension.Command.SiginSuccess(this._jWTTokenHelper.GenerateToken()));
        }

    }
}
