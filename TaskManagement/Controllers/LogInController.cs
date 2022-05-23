using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManagement.Controllers
{
    public class LogInController : Controller
    {
        public UserManager<AppUser> _userManager;
        public RoleManager<AppRole> _roleManager;
        public SignInManager<AppUser> _signInManager;
        public LogInController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public IActionResult LogInUser(string returnUrl)
        {
            TempData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogInUser(UserForLoginDto userForLoginDto)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(userForLoginDto.EMail);
                if (user != null)
                {
                  
                    Microsoft.AspNetCore.Identity.SignInResult result = _signInManager.PasswordSignInAsync(user, userForLoginDto.Password, false, false).Result;
                    IList<string> roles = await _userManager.GetRolesAsync(user);
                    foreach (var item in roles)
                    {

                        if (item.Contains("Yönetici"))
                        {
                            if (TempData["ReturnUrl"] != null)
                            {
                                return Redirect(TempData["ReturnUrl"].ToString());
                            }

                            return RedirectToAction("EmployeeList", "Manager");
                        }
                        else if (item.Contains("Personel"))
                        {
                            if (TempData["ReturnUrl"] != null)
                            {
                                return Redirect(TempData["ReturnUrl"].ToString());
                            }
                            return RedirectToAction("GetTask", "Employee");
                        }
                    }
                    return RedirectToAction("EmployeeList", "Manager");


                }

            }
            else
            {
                ModelState.AddModelError("", "Geçersiz kullanıcı adi veya şifresi");
            }
            return View(userForLoginDto);
        }
    }
}
