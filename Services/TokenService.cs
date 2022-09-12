using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace UsersApi.Services
{
    public class TokenService
    {
        public Token CreateToken(IdentityUser<int> user, string role)
        {
            Claim[] userClaims = new Claim[]
            {
                new Claim("username", user.UserName),
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn"));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(claims: userClaims, signingCredentials: credentials, expires: DateTime.UtcNow.AddHours(1));

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new Token(TokenCategory.Text, tokenString);
        }
    }
}
