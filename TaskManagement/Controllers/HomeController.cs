using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.Controllers
{
    public class HomeController : Controller
    {
    
        public SignInManager<AppUser> _signInManager;
        public HomeController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
       
        }

        public IActionResult Index()
        {
            return View();
        }
        public void LogOut()
        {
            _signInManager.SignOutAsync();
        }
    }
 
}
