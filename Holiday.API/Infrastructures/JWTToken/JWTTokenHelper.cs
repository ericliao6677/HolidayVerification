using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Holiday.API.Infrastructures.JWTToken
{
    public class JWTTokenHelper
    {
        private readonly IConfiguration _configuration;
        //private readonly HttpContext _context;

        public JWTTokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            //_context = context;
        }

        public string GenerateToken(int expireMinutes = 1)
        {
            var issuer = _configuration.GetValue<string>("JwtSettings:Issuer");
            var signKey = _configuration.GetValue<string>("JwtSettings:SignKey");

            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(ClaimTypes.Role, "Customer"));
            claims.Add(new Claim(ClaimTypes.Role, "employee"));
            

            var userClaimIdentity = new ClaimsIdentity(claims);

            // HmacSha256 MUST be larger than 128 bits, so the key can't be too short. At least 16 and more characters.
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor 
            { 
                 Issuer = issuer,
                 Subject = userClaimIdentity,
                 Expires = DateTime.Now.AddMinutes(expireMinutes),
                 SigningCredentials = signingCredentials
            };

            // Generate a JWT securityToken, than get the serialized Token result (string)
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var serializeToken = tokenHandler.WriteToken(securityToken);

            return serializeToken;
        }

       
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix 時間戳是從 1970 年 1 月 1 日 (UTC) 開始的秒數
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var utcDateTime = unixEpoch.AddSeconds(unixTimeStamp);
            var localDateTime = utcDateTime.ToLocalTime();
            return localDateTime;
        }

    }
}
