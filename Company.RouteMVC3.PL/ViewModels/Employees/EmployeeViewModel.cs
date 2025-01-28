using Company.RouteMVC3.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Company.RouteMVC3.PL.ViewModels.Employees
{
	public class EmployeeViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Name is Required")]
		public string Name { get; set; }

		[Range(25, 60, ErrorMessage = "Age Must Be Between 25 and 60")]
		public int? Age { get; set; }

		[RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}", ErrorMessage = "Address Must be in format 123-Street-City-Country")]
		public string Address { get; set; }

		[Required(ErrorMessage = "Salary is Required")]
		[DataType(DataType.Currency)]
		public decimal Salary { get; set; }

		[DataType(DataType.EmailAddress)]
		//[EmailAddress]
		public string Email { get; set; }

		[Phone]
		public string PhoneNumber { get; set; }
		public bool IsActive { get; set; }
		public DateTime HiringDate { get; set; }
		public int? WorkForId { get; set; } // FK
		public Department? WorkFor { get; set; } // Navigational Property - Optional
		public IFormFile? Image { get; set; }
		public string? ImageName { get; set; }
    }
}
