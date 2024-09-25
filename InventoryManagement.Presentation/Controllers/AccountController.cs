using InventoryManagement.DataAccess.Models;
using InventoryManagement.Presentation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;

		public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username!,model.Password!,model.RememberMe,false);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login attempt");

                return View(model);
            }

			return View(model);
		}

		public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
            if(ModelState.IsValid)
            {
                User user = new()
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    Address = model.Address
                };

                var result = await _userManager.CreateAsync(user,model.Password!);

                if (result.Succeeded)
                {
                    Console.WriteLine("Registration Successful");
                    await _signInManager.SignInAsync(user, false);

					return RedirectToAction("Index", "Home");
				}

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }
			return View(model);
		}

		public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
        }
    }
}
