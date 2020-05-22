using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EnglishCards.Host.Services;
using EnglishCards.Model;
using EnglishCards.Model.Repositories;
using EnglishCards.Service.Learn;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace EnglishCards.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void InitDataContext(IServiceCollection services)
        {
            // TODO: Use services.AddDbContext<DataContext>()
            var dataContext = new DataContext(Configuration["ENGCAR_DBCONNECTION"]);

            DataSeeder.SeedSystemData(dataContext);
            DataSeeder.SeedTestData(dataContext);

            services.AddSingleton(dataContext);
            services.AddSingleton(new DataRepository(dataContext));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<RequestContextService, RequestContextService>();

            services.AddControllers();

            services.AddCors();

            InitDataContext(services);

            services.AddSingleton<LearnService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var fileProvider = new PhysicalFileProvider(Path.GetFullPath(Configuration["StaticFilesCatalog"]));
            app.UseDefaultFiles(new DefaultFilesOptions()
            {
                FileProvider = fileProvider
            });
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = fileProvider
            });

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
