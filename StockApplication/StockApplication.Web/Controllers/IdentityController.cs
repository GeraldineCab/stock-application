using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockApplication.Persistence.Entities;
using StockApplication.Web.Models;

namespace StockApplication.Web.Controllers
{
    [AllowAnonymous]
    public class IdentityController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public IdentityController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var signedUser = await _userManager.FindByEmailAsync(loginModel.Email);
                var canSignIn = await _signInManager.CheckPasswordSignInAsync(signedUser, signedUser.Password, false);
                if (canSignIn.Succeeded)
                {
                    await _signInManager.SignInAsync(signedUser, false);
                    return RedirectToAction("Index", "Home");
                }
       
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View("Index");
            }
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return View("Index");
        }
    }
}
