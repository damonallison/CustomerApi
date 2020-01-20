using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using CustomerApi.Models;
using CustomerApi.Repositories;

namespace CustomerApi
{
    /// The startup class builds up a Host object at startup. The host includes:
    ///
    /// * An HTTP Server
    /// * Middleware
    /// * Logging
    /// * DI
    /// * Configuration
    public class Startup
    {
        /// Only the following service types can be injected into the
        /// <c>Startup</c> constructor
        ///
        /// * IWebHostEnvironment
        /// * IHostEnvironment
        /// * IConfiguration
        public Startup(IConfiguration configuration,
                       IWebHostEnvironment webHostEnvironment)

        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;

            Console.WriteLine($"Environment: {WebHostEnvironment.EnvironmentName}");
            Console.WriteLine($"ContentRootPath: {WebHostEnvironment.ContentRootPath}");
        }

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment WebHostEnvironment { get; }

        /// This method gets called by the runtime. Use this method to add
        /// services to the container.
        ///
        /// ASP.NET has a built in DI container, but it could be swapped out
        /// with a different DI container.
        public void ConfigureServices(IServiceCollection services)
        {
            //
            // There are two ways to validate access tokens:
            //
            // 1. Call the Okta API's introspect endpoint.
            //    + Very specific and most secure
            //    - You have to make another API call
            //
            // 2. Use ASP.NET's local JWT validation
            //    + Don't have to call an API
            //    - Less secure

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                options.Authority = "https://dev-201383.okta.com/oauth2/default";
                options.Audience = "api://default";
                options.RequireHttpsMetadata = false;
            });

            // Custom settings
            services.Configure<CustomerDatabaseSettings>(Configuration.GetSection(nameof(CustomerDatabaseSettings)));
            services.AddSingleton<ICustomerDatabaseSettings>(sp => sp.GetRequiredService<IOptions<CustomerDatabaseSettings>>().Value);

            // Repositories
            services.AddSingleton<CustomerRepository>();

            //
            // Generic service registration methods:
            //
            // services.AddTransient
            // services.AddSingleton
            // services.AddScoped

            services.AddControllers();

        }


        /// This method gets called by the runtime. Use this method to configure
        /// the HTTP request pipeline.
        ///
        /// By convention, middleware is added to the pipeline by calling it's
        /// <c>Use...</c> method.
        ///
        /// The environment is controlled an environment variable:
        /// <c>ASPNETCORE_ENVIRONMENT</c>
        /// <c>IHostingEnvironment></c> is available anywhere in the app
        /// via DI.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
