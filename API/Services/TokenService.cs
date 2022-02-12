using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        //Moet een constructor aanmaken omdat de configuratie gebruikt moet worden
        public TokenService(IConfiguration config)
        {
            //De token staat in appsettings.json
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(AppUser user)
        {
            //Claims toevoegen
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            //Credentials aanmaken
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //Beschrijving van hoe de token eruit gaat zien
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            //Dit is verplicht om een token te maken
            var tokenHandler = new JwtSecurityTokenHandler();

            //Het aanmaken van een token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}