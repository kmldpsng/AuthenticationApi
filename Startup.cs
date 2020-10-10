using AuthenticationApi.AuthorizationPolicies;
using AuthenticationApi.Handler;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace AuthenticationApi
{
    public class Startup 
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Authentication API",
                    Description = "A simple demonstration for ASP.NET Core Web API",
                    TermsOfService = new Uri("https://TOS.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Kamal Deep Singh",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/_kmldp"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://no-licenceASofNow.com/license"),
                    }
                });
            });

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthenticationHandler",null);

            //SetUpAuthorization(services);
        }

        private static void SetUpAuthorization(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(); //this will be used for the baseic authentication mechanism

            //services.AddAuthentication(IISDefaults.AuthenticationScheme);
            //services.AddAuthorization(AddUsersAuthorization);
            
        }

        private static void AddUsersAuthorization(AuthorizationOptions authorizationOptions)
        {
            //!_env.IsProduction()
            var userAuthorizationRequirement =
                new UserAuthorizationRequirement(true);
            authorizationOptions.AddPolicy("Add user policy",
                policy => { policy.Requirements.Add(userAuthorizationRequirement); });
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

            app.UseSwagger();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication API");
                //c.RoutePrefix = string.Empty;
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
