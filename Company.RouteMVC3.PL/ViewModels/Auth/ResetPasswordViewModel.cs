using System.ComponentModel.DataAnnotations;

namespace Company.RouteMVC3.PL.ViewModels.Auth
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password is Required !!")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "ConfirmPassword is Required !!")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Confirmed Password Doesn't Match Password !!")]
		public string ConfirmPassword { get; set; }
	}
}
