using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HAKATON.Models;

namespace HAKATON.DataContext
{
	public class AppDBContext : IdentityDbContext<UserViewModel>
	{
		public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
		{ }
	}
}
