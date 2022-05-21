using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TaskManagement.Controllers
{
    public class SignUpController : Controller
    {
   
        public UserManager<AppUser> _userManager;
        public RoleManager<AppRole> _roleManager;

        public SignUpController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
         
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserForRegisterDto register)
        {
            AppUser user = new AppUser();
            user.UserName = register.UserName;
            user.FirstName = register.FirstName;
            user.LastName = register.LastName;
            user.Email = register.EMail;
            IdentityResult result = await _userManager.CreateAsync(user,register.Password);
            if (result.Succeeded)
            {
         
                return RedirectToAction("LogInUser", "LogIn");
            }
            else
            {

                ModelState.AddModelError("", "Hata!");
            }
            
            return View(register);

            
        }
        public void AddModelError(IdentityResult result)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
        }
    }
}
