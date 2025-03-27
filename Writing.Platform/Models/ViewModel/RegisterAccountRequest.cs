using System.ComponentModel.DataAnnotations;

namespace Writing.Platform.Models.ViewModel
{
    public class RegisterAccountRequest
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
