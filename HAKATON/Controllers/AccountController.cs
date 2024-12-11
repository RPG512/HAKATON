using HAKATON.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

		[Authorize]
		public IActionResult Index(string userName)
		{
			var model = _signInManager.UserManager.Users.First(u => u.UserName == userName);
			return View(model);
		}
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Index(UserViewModel model)
		{
			if (ModelState.IsValid)
			{
				UserViewModel user = _userManager.Users.First(u => u.UserName == model.UserName);

				user.Name = model.Name;
				user.UserName = model.UserName;
				user.Email = model.Email;
				user.Contacts = model.Contacts;
				user.PhoneNumber = model.PhoneNumber;
				user.DateOfBirth = model.DateOfBirth;

				var result = await _userManager.UpdateAsync(user);

				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
				else
					foreach (var error in result.Errors)
						ModelState.AddModelError("", error.Description);
			}
			return View(model);
		}

		public new IActionResult User(Guid pid)
		{
			var model = _signInManager.UserManager.Users.First(u => u.PublicId == pid);
			return View(model);
		}

		[Authorize]
		public IActionResult Delete()
		{
			return View();
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Delete(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = _userManager.Users.First(u => u.UserName == model.UserName);
				if (_signInManager.CheckPasswordSignInAsync(user, model.Password, false).Result.Succeeded)
				{
					var result = await _userManager.DeleteAsync(user);

					if (result.Succeeded)
					{
						await _signInManager.SignOutAsync();
						return RedirectToAction("Index", "Home");
					}
				}
				ModelState.AddModelError("", "Недопустимая попытка входа в систему");
			}

			return View(model);
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
