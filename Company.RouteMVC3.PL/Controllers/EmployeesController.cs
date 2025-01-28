using AutoMapper;
using Company.RouteMVC3.BL.Interfaces;
using Company.RouteMVC3.BL.Repositories;
using Company.RouteMVC3.DAL.Models;
using Company.RouteMVC3.PL.Helper;
using Company.RouteMVC3.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.RouteMVC3.PL.Controllers
{
	[Authorize]

	public class EmployeesController : Controller
    {
  //      private readonly IEmployeeRepository _employeeRepository;
		//private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeesController(
            //IEmployeeRepository employeeRepository 
            //, IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
			IMapper mapper
			)
		
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string InputSearch)
        {
            var employees= Enumerable.Empty<Employee>();

			if (string.IsNullOrEmpty(InputSearch))
            {
				employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
			}
            else
            {
				employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(InputSearch);
            }

			var result = _mapper.Map <IEnumerable<EmployeeViewModel>>(employees);

			// View Dictionary : Transfer Data From Action to View (One Way)

			//1. ViewData > Property Inherited From Controller Class , Dictionary
			//ViewData["Data01"] = "Hello World From ViewData";

			////2. ViewBag > Property Inherited From Controller Class , Dynamic
			//ViewBag.Data02 = "Hello World From ViewBag";

			//3. TempData > Property Inherited From Controller Class , Dictionary
			//Transfer Data from Request to Another Request
			//TempData["Data03"] = "Hello World From TempData";

			return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    if (model.Image is not null)
                    {
                        model.ImageName = DocumentSettings.Upload(model.Image, "images");
                    }
                    //Employee employee = new Employee()
                    //{
                    //    Id = model.Id,
                    //    Name = model.Name,
                    //    Address = model.Address,
                    //    Salary = model.Salary,
                    //    Age = model.Age,
                    //    HiringDate = model.HiringDate,
                    //    IsActive = model.IsActive,
                    //    WorkFor = model.WorkFor,
                    //    WorkForId = model.WorkForId,
                    //    Email = model.Email,
                    //    PhoneNumber = model.PhoneNumber,
                    //};
                    var employee = _mapper.Map<Employee>(model);


                    await _unitOfWork.EmployeeRepository.AddAsync(employee);

                    var Count = await _unitOfWork.CompleteAsync();
                    if (Count > 0)
                    {
                        TempData["Message"] = "Employee Created";
                    }
                    else
                    {
                        TempData["Message"] = "Employee Not Created";
                    }
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

			return View(model);
        }

        public async Task<IActionResult> Details(int? id/*, string viewName = "Details"*/)
        {

            try
            {
                if (id is null) return BadRequest(); // 400

                var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);

                if (employee == null) return NotFound(); // 404

                //EmployeeViewModel employeeViewModel = new EmployeeViewModel()
                //{
                //	Id = employee.Id,
                //	Name = employee.Name,
                //	Address = employee.Address,
                //	Salary = employee.Salary,
                //	Age = employee.Age,
                //	HiringDate = employee.HiringDate,
                //	IsActive = employee.IsActive,
                //	WorkFor = employee.WorkFor,
                //	WorkForId = employee.WorkForId,
                //	Email = employee.Email,
                //	PhoneNumber = employee.PhoneNumber,
                //};

                var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);

                return View(employeeViewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error","Home");
            }
        }


        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
				var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();

				ViewData["departments"] = departments;

				if (id is null) return BadRequest(); // 400

				var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);

				if (employee == null) return NotFound();

                //EmployeeViewModel employeeViewModel = new EmployeeViewModel()
                //{
                //	Id = employee.Id,
                //	Name = employee.Name,
                //	Address = employee.Address,
                //	Salary = employee.Salary,
                //	Age = employee.Age,
                //	HiringDate = employee.HiringDate,
                //	IsActive = employee.IsActive,
                //	WorkFor = employee.WorkFor,
                //	WorkForId = employee.WorkForId,
                //	Email = employee.Email,
                //	PhoneNumber = employee.PhoneNumber,
                //};

                var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);
                return View(employeeViewModel);

			} catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel model)
        {
            try
            {

                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    if(model.ImageName is not null)
                    {
						DocumentSettings.Delete(model.ImageName, "images");
					}

                    if(model.Image is not null)
                    {
						model.ImageName = DocumentSettings.Upload(model.Image, "images");
					}

					//Employee employee = new Employee()
					//{
					//	Id = model.Id,
					//	Name = model.Name,
					//	Address = model.Address,
					//	Salary = model.Salary,
					//	Age = model.Age,
					//	HiringDate = model.HiringDate,
					//	IsActive = model.IsActive,
					//	WorkFor = model.WorkFor,
					//	WorkForId = model.WorkForId,
					//	Email = model.Email,
					//	PhoneNumber = model.PhoneNumber,
					//};

					var employee = _mapper.Map<Employee>(model);


                    _unitOfWork.EmployeeRepository.Update(employee);
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

				var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);

				if (employee is null) return NotFound(); // 404


                //EmployeeViewModel employeeViewModel = new EmployeeViewModel()
                //{
                //	Id = employee.Id,
                //	Name = employee.Name,
                //	Address = employee.Address,
                //	Salary = employee.Salary,
                //	Age = employee.Age,
                //	HiringDate = employee.HiringDate,
                //	IsActive = employee.IsActive,
                //	WorkFor = employee.WorkFor,
                //	WorkForId = employee.WorkForId,
                //	Email = employee.Email,
                //	PhoneNumber = employee.PhoneNumber,
                //};

                var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);


                return View(employeeViewModel);

			}
			catch (Exception Ex)
            {
				ModelState.AddModelError(string.Empty, Ex.Message);
				return RedirectToAction("Error","Home");
			}

		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel model)
        {
            try
            {
                if (id != model.Id) return BadRequest();

				if (ModelState.IsValid)
				{
           

					//Employee employee = new Employee()
					//{
					//	Id = model.Id,
					//	Name = model.Name,
					//	Address = model.Address,
					//	Salary = model.Salary,
					//	Age = model.Age,
					//	HiringDate = model.HiringDate,
					//	IsActive = model.IsActive,
					//	WorkFor = model.WorkFor,
					//	WorkForId = model.WorkForId,
					//	Email = model.Email,
					//	PhoneNumber = model.PhoneNumber,
					//};

					var employee = _mapper.Map<Employee>(model);


                    _unitOfWork.EmployeeRepository.Delete(employee);
                    var Count = await _unitOfWork.CompleteAsync();

                    if (Count > 0)
				    {
						if (model.ImageName is not null)
						{
							DocumentSettings.Delete(model.ImageName, "images");
						}
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

    }
}
