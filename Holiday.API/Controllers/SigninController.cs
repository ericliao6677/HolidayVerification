using Azure.Core;
using Holiday.API.Domain.Request.Post;
using Holiday.API.Domain.Response;
using Holiday.API.Infrastructures.JWTToken;
using Holiday.API.Services.Implement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Holiday.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class SigninController : ControllerBase
    {
        private readonly JWTTokenHelper _jWTTokenHelper;
        private readonly MemoryCacheService _cache;


        public SigninController(JWTTokenHelper jWTTokenHelper, MemoryCacheService cache)
        {
            _jWTTokenHelper = jWTTokenHelper;
            _cache = cache;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Signin([FromBody] PostSigninRequest request)
        {
            
            if (request is not { Account: "Admin", Password: "=-09poiu" })
                return Ok(ResultResponseExtension.Verify.LoginVerificationError());
            return Ok(ResultResponseExtension.Command.SiginSuccess(this._jWTTokenHelper.GenerateToken()));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Signout()
        {
            //取得jti
            var jwt_id = User.Claims.FirstOrDefault(p => p.Type == "jti")?.Value;

            //取得過期時間
            var exp = User.Claims.FirstOrDefault(p => p.Type == "exp")?.Value;

            var formatDate = JWTTokenHelper.UnixTimeStampToDateTime(double.Parse(exp));
            var memoryCacheExpired = formatDate - DateTime.Now;

            //將jti加入黑名單
            string jti = _cache.AddToMemoryCache(jwt_id, memoryCacheExpired);
            return Ok(ResultResponseExtension.Command.SignoutSuccess(DateTime.Now));
        }

        [HttpGet]
        public IActionResult CheckCache([FromQuery] string jti)
        {
            return Ok(_cache.GetCache(jti));
        }



    }
}
