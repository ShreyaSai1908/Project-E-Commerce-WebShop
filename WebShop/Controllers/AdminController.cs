using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models.Identity;

namespace WebShop.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminController : Controller
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<UserIdentity> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }
        public async Task<IActionResult> AddAdmin(string id)
        {
            UserIdentity userIdentity = await _userManager.FindByIdAsync(id);

            if (userIdentity != null)
            {
                var result = await _userManager.AddToRoleAsync(userIdentity, "Admin");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveAdmin(string id)
        {
            UserIdentity userIdentity = await _userManager.FindByIdAsync(id);

            if (userIdentity != null && userIdentity.UserName != "WebShopAdmin")
            {
                var result = await _userManager.RemoveFromRoleAsync(userIdentity, "Admin");
            }
            return RedirectToAction("Index");
        }
    }
}
