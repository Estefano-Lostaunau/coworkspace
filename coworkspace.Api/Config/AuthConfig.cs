using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Coworkspace.Api.Config
{
    public static class AuthConfig
    {
        public static readonly string[] PublicEndpoints = new[]
        {
            "/api/auth/register",
            "/api/auth/login",
            "/swagger",
            "/swagger/index.html",
            "/swagger/v1/swagger.json"
        };

        public static bool IsPublicEndpoint(string path)
        {
            return PublicEndpoints.Any(endpoint => 
                path.StartsWith(endpoint, StringComparison.OrdinalIgnoreCase));
        }

        public static void AddJwtConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection("JwtConfig").Get<JwtConfig>();
            if (jwtConfig == null)
            {
                throw new ArgumentNullException(nameof(jwtConfig), "Falta la configuración de JWT.");
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(jwtConfig.Secret))
                };
            });
        }

        public static void AddCorsConfiguration(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }
    }
}