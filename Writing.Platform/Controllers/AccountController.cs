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
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,  SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var model = new LoginAccountRequest
            {
                ReturnUrl = returnUrl,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginAccountRequest loginAccountRequest)
        {
            var sign = signInManager.PasswordSignInAsync(loginAccountRequest.UserName, loginAccountRequest.Password, false, false);

            if(sign != null && sign.Result.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(loginAccountRequest.ReturnUrl))
                {
                    return Redirect(loginAccountRequest.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
                    
            }
            return View(loginAccountRequest);
        }
        [HttpGet]
        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
