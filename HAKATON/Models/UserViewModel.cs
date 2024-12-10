using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HAKATON.Models
{
	public class UserViewModel : IdentityUser
	{
		[Required]
		public string Name { get; set; } = null!;

		public int AvatarId { get; set; } = 0;

		[Display(Name = "Контакты для связи")]
		public string? Contacts { get; set; }
	}
}
