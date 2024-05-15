using System.ComponentModel.DataAnnotations;

namespace Holiday.API.Domain.Request.Post;

public record PostSigninRequest([Required] string Account, [Required] string Password);
