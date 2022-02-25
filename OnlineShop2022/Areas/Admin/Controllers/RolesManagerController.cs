using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace OnlineShop2022.Areas.Admin.Controllers
{
    [Authorize (Roles = "Admin")]
    [Area("Admin")]
    public class RolesManagerController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesManagerController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            var newRole = new IdentityRole
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            };

            if (roleName != null)
            {
                await _roleManager.CreateAsync(newRole);
            }
            return RedirectToAction("Index");
        }


        
        public async Task<IActionResult> DeleteRole(string id)
        {
            //Error here - the logic of the if statement is incorrect. If a valid ID is passed, it will not equal null and Redirect.
            //Fixed the error - changed the if statement logic from "==" to "!-" to work if the ID is not empty. 
            if (id != null)
            {
                var role = await _roleManager.FindByIdAsync(id);
                await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }


    }
}
