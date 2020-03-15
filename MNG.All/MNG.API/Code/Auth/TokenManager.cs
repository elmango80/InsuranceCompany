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
using MNG.Infrastructure.Models;

namespace MNG.API.Code.Auth
{
    public class TokenManager : ITokenManager
    {
        private readonly TokenSetting _tokenSetting;

        public TokenManager(IOptions<TokenSetting> tokenSetting)
        {
            _tokenSetting = tokenSetting.Value;
        }

        public ModelResponse<JWTResult> GetJWT(User currentUser)
        {
            if (currentUser == null)
            {
                throw new ArgumentNullException(string.Empty, MessageValues.ARGUMENT_NULL);
            }

            var result = new ModelResponse<JWTResult>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSetting.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(Convert.ToDouble(_tokenSetting.JwtExpireHours));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, currentUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub, currentUser.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                _tokenSetting.JwtIssuer,
                _tokenSetting.JwrAudience,
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            result.Model = new JWTResult
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };

            return result;
        }
    }
}
