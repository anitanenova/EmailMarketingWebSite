using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessManagement.WebSite
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.Extensions.Options;
    using Services;
    using Services.Interfaces;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {

            var builder = new ConfigurationBuilder()
                //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc()
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                )
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var baseUrl = Configuration["ApiBaseUrl"];
            services.AddTransient<IBlogPostService, BlogPostService>(f=> new BlogPostService(baseUrl));
            services.AddTransient<IBlogTagService, BlogTagService>(f => new BlogTagService(baseUrl));

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                .AddViewLocalization(
                    LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = "Resources"; })
                .AddDataAnnotationsLocalization();


            services.Configure<RequestLocalizationOptions>(
                opts =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-US"),
                        new CultureInfo("bg-BG"),
                        new CultureInfo("fr-FR")
                    };

                    opts.DefaultRequestCulture = new RequestCulture("en-US");
                    // Formatting numbers, dates, etc.
                    opts.SupportedCultures = supportedCultures;
                    // UI strings that we have localized.
                    opts.SupportedUICultures = supportedCultures;
                });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
