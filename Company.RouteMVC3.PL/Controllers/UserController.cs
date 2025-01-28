using Company.RouteMVC3.DAL.Models;
using Company.RouteMVC3.PL.Helper;
using Company.RouteMVC3.PL.ViewModels;
using Company.RouteMVC3.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.RouteMVC3.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }



		//public async Task<IActionResult> Index(string InputSearch)
		//{
		//    var users = Enumerable.Empty<UserViewModel>();

		//    if (string.IsNullOrEmpty(InputSearch))
		//    {
		//        users = await _userManager.Users.Select(u => new UserViewModel()
		//        {
		//            Id = u.Id,
		//            FirstName = u.FirstName,
		//            LastName = u.LastName,
		//            Email = u.Email,
		//            Roles = _userManager.GetRolesAsync(u).Result
		//        }).ToListAsync();


		//    }
		//    else
		//    {
		//        users = await _userManager.Users.Where(u => u.Email
		//        .ToLower()
		//        .Contains(InputSearch.ToLower()))
		//        .Select(u => new UserViewModel()
		//        {
		//            Id = u.Id,
		//            FirstName = u.FirstName,
		//            LastName = u.LastName,
		//            Email = u.Email,
		//            Roles = _userManager.GetRolesAsync(u).Result
		//        }).ToListAsync();
		//    }

		//    return View(users);
		//}



		public async Task<IActionResult> Index(string InputSearch)
		{
			IEnumerable<UserViewModel> users;

			// Fetch users based on the search input or all users if no input
			if (string.IsNullOrEmpty(InputSearch))
			{
				users = await _userManager.Users
					.Select(u => new UserViewModel
					{
						Id = u.Id,
						FirstName = u.FirstName,
						LastName = u.LastName,
						Email = u.Email
					})
					.ToListAsync();
			}
			else
			{
				users = await _userManager.Users
					.Where(u => u.Email.ToLower().Contains(InputSearch.ToLower()))
					.Select(u => new UserViewModel
					{
						Id = u.Id,
						FirstName = u.FirstName,
						LastName = u.LastName,
						Email = u.Email
					})
					.ToListAsync();
			}

			// Now, for each user, asynchronously fetch their roles
			foreach (var user in users)
			{
				user.Roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user.Id));
			}

			return View(users);
		}


		public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {

            if (id is null) return BadRequest(); // 400

            var userFromDb = await _userManager.FindByIdAsync(id);

            if (userFromDb is null) return NotFound();

            var user = new UserViewModel()
            {
                Id = userFromDb.Id, 
                FirstName = userFromDb.FirstName,
                LastName = userFromDb.LastName,
                Email = userFromDb.Email,
                Roles = _userManager.GetRolesAsync(userFromDb).Result
            };

            return View(viewName, user);

        }


        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel model)
        {
            if (id != model.Id) return BadRequest();

          
                var userFromDb = await _userManager.FindByIdAsync(id);

                if (userFromDb is null) return NotFound();

                userFromDb.FirstName = model.FirstName;
                userFromDb.LastName = model.LastName;
                userFromDb.Email = model.Email;

                await _userManager.UpdateAsync(userFromDb);

                return RedirectToAction(nameof(Index));
         

        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, UserViewModel model)
        {
            if (id != model.Id) return BadRequest();

         
                var userFromDb = await _userManager.FindByIdAsync(id);

                if (userFromDb is null) return NotFound();

                await _userManager.DeleteAsync(userFromDb);

                return RedirectToAction(nameof(Index));
     

        }

    }
}
