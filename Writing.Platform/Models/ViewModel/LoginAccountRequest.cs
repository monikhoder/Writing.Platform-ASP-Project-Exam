namespace Writing.Platform.Models.ViewModel
{
    public class LoginAccountRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
