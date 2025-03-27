using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Writing.Platform.Data;
using Writing.Platform.Models.ViewModel;

namespace Writing.Platform.Controllers
{
    public class AdminUserController : Controller
    {
        private readonly AuthDbContext authDbContext;
        private readonly UserManager<IdentityUser> userManager;

        public AdminUserController(AuthDbContext authDbContext, UserManager<IdentityUser> userManager)
        {
            this.authDbContext = authDbContext;
            this.userManager = userManager;
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var users = await authDbContext.Users.ToListAsync();
  
            var userViewModels = new UserViewModel();
            userViewModels.Users = new List<User>();

            foreach (var user in users)
            {
                if(user.Email != "admin@writing.platform")
                {
                    userViewModels.Users.Add(new User
                    {
                        Id = Guid.Parse(user.Id),
                        UserName = user.UserName,
                        Email = user.Email,
                    });

                }
                
            }

            return View(userViewModels);
        }
        [HttpPost]
        public async Task<IActionResult> List(UserViewModel userViewModel)
        {
            var newUser = new IdentityUser
            {
                UserName = userViewModel.Username,
                Email = userViewModel.Email,
            };
            var AddUser = await userManager.CreateAsync(newUser, userViewModel.Password);

            if (AddUser.Succeeded)
            {
                var roles = new List<string> { "User" };
                if (userViewModel.AuthorRole)
                {
                    roles.Add("Author");
                }
                if (userViewModel.AdminRole)
                {
                    roles.Add("Admin");
                }
                AddUser = await userManager.AddToRolesAsync(newUser, roles);
                if (AddUser.Succeeded)
                {
                    return RedirectToAction("List", "AdminUser");
                }

            }

            return View("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {

            var user = await userManager.FindByIdAsync(id.ToString());

            if(user is not null)
            {
               var result = await userManager.DeleteAsync(user);

                if(result is not null && result.Succeeded)
                {
                    return RedirectToAction("List", "AdminUser");
                }
            }

            return View();

        }

       
    }
}
