using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Internship.Persistence.Repositories
{
	public interface IJwtTokenGenerator
	{
		public JwtSecurityToken GenerateJwtToken(List<Claim> authClaims);
	}
}