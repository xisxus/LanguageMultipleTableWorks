using LanguageInstall.Data.Model;
using LanguageInstall.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanguageInstallMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Login and Register pages don't require authentication
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(string email, string password)
        {
            var user = _userService.Login(email, password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserName", user.Name);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid email or password.");
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (_userService.Register(user))
                    return RedirectToAction("Login");
                ModelState.AddModelError("", "Email already exists.");
            }
            return View(user);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
