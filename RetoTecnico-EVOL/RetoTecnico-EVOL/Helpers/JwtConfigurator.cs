using Microsoft.IdentityModel.Tokens;
using RetoTecnico_EVOL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RetoTecnico_EVOL.Helpers
{
    public class JwtConfigurator
    {
        public static string GetToken(User user, IConfiguration _config)
        {

            string SecretKey = _config["Jwt:SecretKey"];
            string Issuer = _config["Jwt:Issuer"];
            string Audience = _config["Jwt:Audience"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
         new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
         new Claim("UserId",user.Id.ToString()),
         new Claim("GoRestId",user.GoRestId.ToString()),
         new Claim("UserName",user.Name.ToString()),
     };

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
                );
            var response = new JwtSecurityTokenHandler().WriteToken(token);
            return response;
        }
    }
}
