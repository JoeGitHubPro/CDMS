namespace System.Api.Models.Identity
{
    public class ConfirmEmailModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }

}