using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Judemy.Api.Services
{
	public interface IJwtTokenGenerator
	{
		public JwtSecurityToken GenerateJwtToken(List<Claim> authClaims);
	}
}