using Organic.Services.AuthAPI.Models;

namespace Organic.Services.AuthAPI.Services.IService
{
	public interface IJwtTokenGenerator
	{
		string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
	}
}
