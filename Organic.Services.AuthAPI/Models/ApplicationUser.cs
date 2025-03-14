using Microsoft.AspNetCore.Identity;

namespace Organic.Services.AuthAPI.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
	}
}
