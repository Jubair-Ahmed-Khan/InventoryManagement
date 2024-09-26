using InventoryManagement.DataAccess.Models;
using InventoryManagement.Presentation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagement.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
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
			var roles = _roleManager.Roles.Select(r => new SelectListItem
			{
				Value = r.Name,
				Text = r.Name
			}).ToList();

			// Pass the roles to the view model
			var model = new RegisterViewModel
			{
				RoleList = roles
			};

			return View(model);
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
                    await _userManager.AddToRoleAsync(user,model.Role);

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
