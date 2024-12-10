using System.ComponentModel.DataAnnotations;

namespace HAKATON.Models.Account
{
	public class RegisterViewModel
	{
		[Required]
		[Display(Name = "Имя")]
		public string Name { get; set; } = null!;

		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; } = null!;

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Пароль")]
		public string? Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Подтверждение пароля")]
		[Compare("Password", ErrorMessage = "Пароли не совпадают!")]
		public string? ConfirmPassword { get; set; }

		[Display(Name = "Контакты для связи (не обязательно)")]
		public string? Contacts { get; set; }
	}
}
