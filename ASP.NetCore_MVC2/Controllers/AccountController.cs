using ASP.NetCore_MVC2.Models;
using ASP.NetCore_MVC2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ASP.NetCore_MVC2.Controllers
{
   
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ApplicationUser> _UserManager,SignInManager<ApplicationUser> _SignInManager,RoleManager<IdentityRole> roleManager)
        {
            userManager = _UserManager;
            signInManager = _SignInManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel UserVM)
        {
            if(ModelState.IsValid)
            {
                //check
               ApplicationUser userModel= await userManager.FindByNameAsync(UserVM.UserName);
                if(userModel != null)
                {
                   bool found = await userManager.CheckPasswordAsync(userModel, UserVM.Password);
                    if(found)
                    {
                        // await signInManager.SignInAsync(userModel, UserVM.RememberMe);
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim("Address", userModel.Address));
                        await signInManager.SignInWithClaimsAsync
                             (userModel, UserVM.RememberMe,claims);
                        return RedirectToAction("Index", "Instructor");
                    }
                }
                ModelState.AddModelError("", "UserName and Password InValid");
            }
            return View(UserVM);
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterUserViewModel model = new RegisterUserViewModel();
            model.ApplicationRoles = roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel newUserVM)
        {
            if(ModelState.IsValid)
            {
                //create acount
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = newUserVM.UserName;
                userModel.PasswordHash = newUserVM.Password;
                userModel.Address = newUserVM.Address;
                IdentityResult result=await userManager.CreateAsync(userModel,newUserVM.Password);
                if(result.Succeeded == true)
                {
                    //create cookie
                    //await signInManager.SignInAsync(userModel, false);
                    //return RedirectToAction("Index", "Instructor");
                    IdentityRole identityRole = 
                        await roleManager.FindByIdAsync(newUserVM.ApplicationRoleId);
                    if (identityRole != null)
                    {
                        IdentityResult roleResult = 
                            await userManager.AddToRoleAsync(userModel, identityRole.Name);
                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction("Index","Instructor");
                        }
                    }
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(newUserVM);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
