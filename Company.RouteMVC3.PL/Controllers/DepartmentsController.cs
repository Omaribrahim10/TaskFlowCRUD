using Company.RouteMVC3.BL.Interfaces;
using Company.RouteMVC3.BL.Repositories;
using Company.RouteMVC3.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.RouteMVC3.PL.Controllers
{
    [Authorize]
	public class DepartmentsController : Controller
	{
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentRepository;


        public DepartmentsController(/*IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork)
        {
			//_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }
		[HttpGet]
        public async Task<IActionResult> Index()
		{
			var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
			return View(departments);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Department model)
        {
			if (ModelState.IsValid)
			{
                try
                {
                    await _unitOfWork.DepartmentRepository.AddAsync(model);
                    var Count = await _unitOfWork.CompleteAsync();

                    if (Count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
 
            }

            return View(model);


        }

		public async Task<IActionResult> Details(int? id)
		{
            try
            {
                if (id is null) return BadRequest(); // 400

                var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);

                if (department == null) return NotFound(); // 404

                return View(department);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }

        }
		[HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id is null) return BadRequest(); // 400

                var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);

                if (department == null) return NotFound(); // 404

                return View(department);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int? id ,Department model)
        {
			try
			{
                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    _unitOfWork.DepartmentRepository.Update(model);
                    var Count = await _unitOfWork.CompleteAsync();

                    if (Count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            } 
            catch (Exception Ex)
            {
                ModelState.AddModelError(string.Empty, Ex.Message);
            }
            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id is null) return BadRequest(); // 400

                var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);

                if (department == null) return NotFound(); // 404

                return View(department);
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(string.Empty, Ex.Message);
                return RedirectToAction("Error", "Home");

            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> Delete([FromRoute]int? id, Department model)
        {
            try
            {
                if (id != model.Id) return BadRequest();

                //if (ModelState.IsValid)
                //{
                //    var department = _departmentRepository.Get(id.Value);
                //    if (department == null) return NotFound(); // Ensure the record exists

                //    var Count = _departmentRepository.Delete(department);
                //    if (Count > 0)
                //    {
                //        return RedirectToAction(nameof(Index));
                //    }
                //}

                // Fetch the full model from the database
                var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
                if (department == null) return NotFound(); // Ensure the record exists

                // Proceed with deletion
                _unitOfWork.DepartmentRepository.Delete(department);
                var Count = await _unitOfWork.CompleteAsync();

                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(string.Empty, Ex.Message);
            }
            return View(model);

        }

    }
}
