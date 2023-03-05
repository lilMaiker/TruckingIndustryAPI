using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TruckingIndustryAPI.Entities.DTO.Request;
using TruckingIndustryAPI.Entities.Models.Identity;
using TruckingIndustryAPI.Extensions.Attributes;

namespace TruckingIndustryAPI.Helpers
{
    /// <summary>
    /// Class to handle JWT generation and verification
    /// </summary>
    /// 
    [ServiceLifetime(ServiceLifetime.Scoped)]
    public class JwtHandlerService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        private readonly IConfigurationSection _goolgeSettings;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtHandlerService(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
            _goolgeSettings = _configuration.GetSection("GoogleAuthSettings");
            _userManager = userManager;
        }

        /// <summary>
        /// Generates a JSON Web Token for a given user.
        /// </summary>
        /// <param name="user">The user for which the JWT will be generated.</param>
        /// <returns>The generated JWT as a string.</returns>
        public async Task<string> GenerateToken(ApplicationUser user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }

        /// <summary>
        /// Verifies the Google token and returns the payload if it's valid.
        /// </summary>
        /// <param name="externalAuth">The external authentication data containing the ID token to validate.</param>
        /// <returns>The Google Json Web Signature payload if the token is valid, or null if the token is invalid or an error occurs during validation.</returns>
        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthDto externalAuth)
        {
            if (externalAuth == null)
            {
                throw new ArgumentNullException(nameof(externalAuth), "The external authentication data cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(externalAuth.IdToken))
            {
                throw new ArgumentException("The ID token cannot be null or empty.", nameof(externalAuth.IdToken));
            }

            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(externalAuth.IdToken);
                return payload;
            }
            catch (Exception ex)
            {
                // log the general exception
                return null;
            }
        }


        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings["validIssuer"],
                audience: _jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["expiryInMinutes"])),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }
    }
}
