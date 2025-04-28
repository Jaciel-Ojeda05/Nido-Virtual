using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UNITEC.NV.API.Configuration
{
    public static class JWTConfiguration
    {
        public static TokenValidationParameters ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            string secretKey = configuration.GetSection("AppSettings").GetValue<string>("JWTSecret")?.Trim() ?? "";
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            return tokenValidationParameters;
        }
    }
}
