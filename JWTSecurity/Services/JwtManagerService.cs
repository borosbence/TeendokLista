using JWTSecurity.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JWTSecurity.Services
{
    public class JwtManagerService
    {
        // Read from appsettings.json
        private readonly IConfiguration _configuration;

        public JwtManagerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generate a JWT token with the username, and one specific role.
        /// </summary>
        /// <param name="userName">username</param>
        /// <param name="role">user role</param>
        /// <returns>JwtToken</returns>
        public JwtToken GenerateToken(string userName, string role = null)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role)
            };
            var signIn = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        // expires: DateTime.UtcNow.AddMinutes(10),
                        expires: DateTime.UtcNow.AddMinutes(1),
                        signingCredentials: signIn);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = GenerateRefreshToken();
            return new JwtToken { Access_Token = accessToken, Refresh_Token = refreshToken };
        }

        /// <summary>
        /// Generate a random refresh token.
        /// </summary>
        /// <returns>BASE64 Encoded random number</returns>
        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }

        /// <summary>
        /// Gets Principal claims from expired access token.
        /// </summary>
        /// <param name="accessToken">JWT access token value</param>
        /// <returns>ClaimsPrincipal</returns>
        /// <exception cref="SecurityTokenException"></exception>
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(key),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token.");
            }

            return principal;
        }
    }
}
