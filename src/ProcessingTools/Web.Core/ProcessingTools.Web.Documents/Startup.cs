using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using ProcessingTools.Web.Documents.Data;
using ProcessingTools.Web.Documents.Models;
using ProcessingTools.Web.Documents.Services;

namespace ProcessingTools.Web.Documents
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            if (env.IsDevelopment() || env.IsStaging())
            {
                this.ServeStaticFiles(app, env, "node_modules/jquery/dist", "/lib/jquery/dist");
                this.ServeStaticFiles(app, env, "node_modules/jquery-validation/dist", "/lib/jquery-validation/dist");
                this.ServeStaticFiles(app, env, "node_modules/jquery-validation-unobtrusive", "/lib/jquery-validation-unobtrusive");
                this.ServeStaticFiles(app, env, "node_modules/bootstrap", "/lib/bootstrap");
                this.ServeStaticFiles(app, env, "node_modules/davidshimjs-qrcodejs", "/lib/qrcodejs");
            }

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void ServeStaticFiles(IApplicationBuilder app, IHostingEnvironment env, string rootPath, string requestPath)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, rootPath)),
                RequestPath = requestPath
            });
        }
    }
}
