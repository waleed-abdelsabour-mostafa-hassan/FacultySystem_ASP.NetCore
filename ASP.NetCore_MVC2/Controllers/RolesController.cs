using ASP.NetCore_MVC2.Models;
using ASP.NetCore_MVC2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetCore_MVC2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        //create Role
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(RoleViewModel newRoleVM)
        {
            if(ModelState.IsValid)
            {
                IdentityRole role=new IdentityRole();
                role.Name = newRoleVM.RoleName;
               IdentityResult result= await roleManager.CreateAsync(role);
                if(result.Succeeded)
                {
                    return View(new RoleViewModel());
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(newRoleVM);
        }
    }
}
