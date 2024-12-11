using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HAKATON.Models
{
	public class UserViewModel : IdentityUser
	{
		public Guid PublicId { get; set; } = Guid.NewGuid();
		[Required]
		[Display(Name = "Имя")]
		public string Name { get; set; } = null!;

		[Display(Name = "Пол")]
		public string? Gender { get; set; }

		[Display(Name = "Учебное заведение/Компания")]
		public string? EducationalInstitutionOrCompany { get; set; }

		public int AvatarId { get; set; } = 0;

		[DataType(DataType.Date)]
		[Display(Name = "Дата рождения")]
		public DateOnly? DateOfBirth { get; set; }

		[Display(Name = "Контакты для связи")]
		public string? Contacts { get; set; }
	}
}
