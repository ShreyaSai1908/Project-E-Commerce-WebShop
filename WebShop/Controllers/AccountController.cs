using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models.Identity;
using WebShop.Models.Services;
using WebShop.Models.ViewModel;

namespace WebShop.Controllers
{
    [Authorize]

    public class AccountController : Controller
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signInManager;

        public AccountController(UserManager<UserIdentity> userManager, SignInManager<UserIdentity> signInManager)
        {            
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false); //username, password, persistlogin, logout


                if (result.Succeeded )
                {                    
                    return RedirectToAction("Index", "Home");
                }                
                else 
                {
                    ViewBag.Msg = "Failed to login.";
                }

            }
            return View(login);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> SignUp(CreateViewModel identityCreate, string firstName, string lastName)
        {
            if (ModelState.IsValid)
            {

                UserIdentity userIdentity = new UserIdentity();
                userIdentity.UserName = identityCreate.UserName;
                userIdentity.FirstName = firstName;
                userIdentity.LastName = lastName;
                userIdentity.PhoneNumber = identityCreate.PhoneNumber;

                var result = await _userManager.CreateAsync(userIdentity, identityCreate.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }

                ViewBag.Msg = "Failed to sign up.";

            }

            return View(identityCreate);
        }

        //[Authorize] not needed here because the controller is already set to authorize. 
        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("ShowProduct", "Product");
        }                
    }
}
