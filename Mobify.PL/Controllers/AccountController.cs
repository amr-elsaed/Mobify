using Microsoft.AspNetCore.Mvc;
using Mobify.BLL.ModelVM.AccountVM;
using Mobify.BLL.Services.Abstraction;
using Mobify.BLL.Services.Implmentation;

namespace Mobify.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountServices accountServices;

        public AccountController(IAccountServices accountServices)
        {
            this.accountServices = accountServices;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogInVM user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var result = await accountServices.SignIn(user);
            if (result)
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Product");
                }
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View(user);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterVM user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var result = await accountServices.Register(user);
            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Unable to create the account with the provided details.");
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await accountServices.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
