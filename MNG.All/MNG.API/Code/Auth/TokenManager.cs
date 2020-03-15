using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using MNG.API.Code.Configuration;
using MNG.API.Code.Contracts;
using MNG.API.Models;
using MNG.Domain.Values;

namespace MNG.API.Code.Auth
{
    public class TokenManager : ITokenManager
    {
        private readonly TokenSetting _tokenSetting;

        public TokenManager(IOptions<TokenSetting> tokenSetting)
        {
            _tokenSetting = tokenSetting.Value;
        }

        public JWTResult GetJWT(User currentUser)
        {
            if (currentUser == null)
            {
                throw new ArgumentNullException(string.Empty, MessageValues.ARGUMENT_NULL);
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSetting.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(Convert.ToDouble(_tokenSetting.JwtExpireHours));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, currentUser.IdClient),
                new Claim(JwtRegisteredClaimNames.Email, currentUser.Email),
                new Claim(ClaimTypes.Name, currentUser.Name),
                new Claim(ClaimTypes.Role, currentUser.Role),
                //new Claim(JwtRegisteredClaimNames.Sub, currentUser.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            var token = new JwtSecurityToken(
                _tokenSetting.JwtIssuer,
                _tokenSetting.JwrAudience,
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JWTResult
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }
    }
}
