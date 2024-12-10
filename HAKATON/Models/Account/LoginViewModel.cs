using System.ComponentModel.DataAnnotations;

namespace My_home_server_Web_Site.Models
{
    public class LoginViewModel
    {
        [Required]
		[Display(Name = "Логин")]
		public string UserName { get; set; } = null!;
		[Required]
        [DataType(DataType.Password)]
		[Display(Name = "Пароль")]
		public string Password { get; set; } = null!;
		[Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; } = false;
    }
}
