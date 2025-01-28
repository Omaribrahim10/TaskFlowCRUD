using Company.RouteMVC3.DAL.Models;
using Company.RouteMVC3.PL.Helper;
using Company.RouteMVC3.PL.ViewModels.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Common;

namespace Company.RouteMVC3.PL.Controllers
{

    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(
			UserManager<ApplicationUser> userManager ,
			SignInManager<ApplicationUser> signInManager
			)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}
		//Sign Up
		#region SignUp
		[HttpGet] // Account/SignUp
		public IActionResult SignUp()

		{
			return View();
		}
		[HttpPost] // Account/SignUp
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var user = await _userManager.FindByNameAsync(model.UserName);
					if (user is null)
					{
						user = await _userManager.FindByEmailAsync(model.Email);
						if (user is null)
						{
							user = new ApplicationUser()
							{
								UserName = model.UserName,
								FirstName = model.FirstName,
								LastName = model.LastName,
								Email = model.Email,
								IsAgree = model.IsAgree,
							};
							var result = await _userManager.CreateAsync(user, model.Password);
							if (result.Succeeded)
							{
								return RedirectToAction("SignIn");
							}
							foreach (var error in result.Errors)
							{
								ModelState.AddModelError(string.Empty, error.Description);
							}

						}
						ModelState.AddModelError(string.Empty, "Email is already exists !");
						return View(model);

					}
					ModelState.AddModelError(string.Empty, "UserName is already exists !");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);

				}

			}
			return View(model);
		}
		#endregion

		#region SignIn
		[HttpGet]
		public IActionResult SignIn()
		{

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var user = await _userManager.FindByEmailAsync(model.Email);
					if (user is not null)
					{
						//check password

						var flag = await _userManager.CheckPasswordAsync(user, model.Password);
						if (flag)
						{
							//sign in

							var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

							if (result.Succeeded)
							{
								return RedirectToAction("Index", "Home");

							}

						}

					}
					ModelState.AddModelError(string.Empty, "Invalid Login !");

				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);

				}

			}
			return View(model);
		}

		#endregion

		#region SignOut
		public new async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}
		#endregion

		#region Forget Password
		[HttpGet]
		public IActionResult ForgetPassword()
		{

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SendResetPasswordURL(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);

				if (user is not null)
				{
					//Create Token

					var token = await _userManager.GeneratePasswordResetTokenAsync(user);

					//create URL for reset password

					var url = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);

					//Create Email

					var email = new DAL.Models.Email()
					{
						To = model.Email,
						Subject = "Reset Password",
						Body = url
					};



					//Send Email

					EmailSettings.SendEmail(email);

					return RedirectToAction(nameof(CheckYourInbox));

				}
				ModelState.AddModelError(string.Empty, "Invalid Operation , Please Try Again !");

			}
			return View(model);
		}

		#endregion

		#region Reset Password
		[HttpGet]
		public IActionResult CheckYourInbox()
		{
			return View();
		}

		[HttpGet]
		public IActionResult ResetPassword(string email, string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["email"] as string;
				var token = TempData["token"] as string;

				var user = await _userManager.FindByEmailAsync(email);
				if (user is not null)
				{
					var result = await _userManager.ResetPasswordAsync(user, token, model.Password);

					if (result.Succeeded)
					{
						return RedirectToAction(nameof(SignIn));
					}
				}

			}

			ModelState.AddModelError(string.Empty, "Invalid Operation , Please Try Again !");

			return View(model);
		} 
		#endregion

		public IActionResult AccessDenied()
		{
			return View();
		}

	}
}
