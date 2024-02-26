using JwtSecurity.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JwtSecurity.Services
{
    public class JwtManagerService
    {
        // Read from appsettings.json
        public readonly IConfiguration _configuration;

        public JwtManagerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generate a JWT token with claims.
        /// </summary>
        /// <param name="claims">Claims</param>
        /// <returns>JwtToken</returns>
        public JwtModel GenerateToken(List<Claim> claims)
        {
            byte[] key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"] ?? string.Empty);
            var signIn = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                        _configuration["JWT:Issuer"],
                        _configuration["JWT:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        signingCredentials: signIn);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new JwtModel(accessToken, GenerateRefreshToken());
        }

        /// <summary>
        /// Generate a random refresh token.
        /// </summary>
        /// <returns>BASE64 Encoded random number</returns>
        public static string GenerateRefreshToken()
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
            byte[] key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"] ?? string.Empty);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                ValidAudience = _configuration["JWT:Audience"],
                ValidIssuer = _configuration["JWT:Issuer"],
                ValidateLifetime = false,
                IssuerSigningKey = new SymmetricSecurityKey(key),
            };
            var principal = new JwtSecurityTokenHandler().ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token.");
            }
            return principal;
        }
    }
}
