using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Writing.Platform.Data;
using Writing.Platform.Models.ViewModel;

namespace Writing.Platform.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public AccountController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterAccountRequest registerRequest)
        {
            var newUser = new IdentityUser
            {
                UserName = registerRequest.userName,
                Email = registerRequest.Email,
            };
            var register = userManager.CreateAsync(newUser, registerRequest.Password);
            if (register.Result.Succeeded)
            {
                var addRole = userManager.AddToRoleAsync(newUser, "User");
                if (addRole.Result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                
            }
            return View();
        }
    }
}
