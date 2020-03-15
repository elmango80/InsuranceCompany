using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

using MNG.API.Code.Auth;
using MNG.API.Code.Configuration;
using MNG.API.Code.Contracts;
using MNG.Application.Contracts;
using MNG.Application.Services;
using MNG.Domain.Entities;
using MNG.Infrastructure.Contracts;
using MNG.Infrastructure.Repositories;

namespace MNG.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var tokenSettingsSection = _configuration.GetSection(ConfigurationKeys.TokenSetting);
            var tokenSetting = tokenSettingsSection.Get<TokenSetting>();

            services.AddControllers();

            services.AddTransient<IRepository<Client>, ClientsRespository>();
            services.AddTransient<IRepository<Policy>, PoliciesRespository>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IPolicyService, PolicyService>();

            services.Configure<TokenSetting>(tokenSettingsSection);
            services.AddAuthentication(config =>
                {
                    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(config =>
                {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = tokenSetting.JwtIssuer,
                        ValidAudience = tokenSetting.JwrAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSetting.JwtKey))
                    };
                });

            services.AddScoped<ITokenManager, TokenManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
