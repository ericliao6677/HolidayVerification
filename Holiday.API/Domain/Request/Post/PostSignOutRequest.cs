using System.ComponentModel.DataAnnotations;

namespace Holiday.API.Domain.Request.Post
{
    public class PostSignOutRequest
    {
        [Required]
        public string? Token { get; set; }
    }
}
