using System.ComponentModel.DataAnnotations;

namespace System.Api.Models.Identity
{

    public class TokenRequestModel
    {
        [EmailAddress]
        public string? Email { get; set; }

        public string? UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}