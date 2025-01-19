using Company.RouteMVC3.BL.Interfaces;
using Company.RouteMVC3.BL.Repositories;
using Company.RouteMVC3.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.RouteMVC3.PL.Controllers
{
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
        public IActionResult Index()
		{
			var departments = _unitOfWork.DepartmentRepository.GetAll();
			return View(departments);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Department model)
        {
			if (ModelState.IsValid)
			{
                _unitOfWork.DepartmentRepository.Add(model);
                var Count = _unitOfWork.Complete();

                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);


        }

		public IActionResult Details(int? id , string viewName="Details")
		{
			if(id is null) return BadRequest(); // 400
			
			var department = _unitOfWork.DepartmentRepository.Get(id.Value);
			
			if (department == null) return NotFound(); // 404

			return View(viewName,department);

		}
		[HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id is null) return BadRequest(); // 400

            //var department = _departmentRepository.Get(id.Value);

            //if (department == null) return NotFound(); // 404

            return Details(id , "Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int? id ,Department model)
        {
			try
			{
                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    _unitOfWork.DepartmentRepository.Update(model);
                    var Count = _unitOfWork.Complete();

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
        public IActionResult Delete(int? id)
        {
            //if (id is null) return BadRequest(); // 400

            //var department = _departmentRepository.Get(id.Value);

            //if (department == null) return NotFound(); // 404

            //return View(department);

            return Details(id, "Delete");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         public IActionResult Delete([FromRoute]int? id, Department model)
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
                var department = _unitOfWork.DepartmentRepository.Get(id.Value);
                if (department == null) return NotFound(); // Ensure the record exists

                // Proceed with deletion
                _unitOfWork.DepartmentRepository.Delete(department);
                var Count = _unitOfWork.Complete();

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
