using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using OdeToFood.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public delegate IRepository ServiceResolver(string key);

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllers();
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
            //services.AddScoped<IGuidService, FixedGuidService>();
            //services.AddScoped<IGuidService, GuidService>();
            //services.AddScoped<IGuidService, GuidService>();

            services.TryAddEnumerable(ServiceDescriptor.Transient<IGuidService, GuidService>());
            services.TryAddEnumerable(ServiceDescriptor.Transient<IGuidService, GuidService>());
            services.TryAddEnumerable(ServiceDescriptor.Transient<IGuidService, FixedGuidService>());

            // Replaces a dependency
            //services.Replace(ServiceDescriptor.Singleton<IGuidService, FixedGuidService>());

            // Another way of registering dependencies.
            //ServiceDescriptor serviceDescriptor = new ServiceDescriptor(typeof(IGuidService), 
            //        typeof(GuidService), ServiceLifetime.Singleton);

            // Remove all dependency
            //services.RemoveAll<IGuidService>();

            // Registering Dependencies and resolving with condition
            services.AddTransient<Repo1>();
            services.AddTransient<Repo2>();

            services.AddTransient<ServiceResolver>(serviceProvider  => key =>
            {
                switch (key)
                {
                    case "1":
                        return serviceProvider.GetService<Repo1>();
                    case "2":
                        return serviceProvider.GetService<Repo2>();
       
                    default:
                       return serviceProvider.GetService<Repo1>(); 
                }
            });

            services.AddDbContextPool<OdeToFoodContext>(options=>
            {
                options.UseSqlServer(Configuration.GetConnectionString("OdeToFoodDb"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseNodeModules();
            app.UseRouting();
            app.UseCookiePolicy();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
