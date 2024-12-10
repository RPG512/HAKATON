using HAKATON.Models;
using HAKATON.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using My_home_server_Web_Site.Models;

namespace HAKATON.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<UserViewModel> _signInManager;
		private readonly UserManager<UserViewModel> _userManager;

		public AccountController(SignInManager<UserViewModel> signInManager, UserManager<UserViewModel> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				UserViewModel user = new()
				{
					Name = model.Name,
					UserName = model.Email,
					Email = model.Email,
					Contacts = model.Contacts
				};

				var result = await _userManager.CreateAsync(user, model.Password!);

				if (result.Succeeded)
				{
					await _signInManager.SignInAsync(user, false);
					return RedirectToAction("Index", "Home");
				}
				else
					foreach (var error in result.Errors)
						ModelState.AddModelError("", error.Description);
			}
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(model.UserName!, model.Password!, model.RememberMe, false);

				if (result.Succeeded)
					return RedirectToAction("Index", "Home");
				ModelState.AddModelError("", "Недопустимая попытка входа в систему");
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
