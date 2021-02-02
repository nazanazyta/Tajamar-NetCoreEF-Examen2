using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProyectoLimpioCore.Data;
using ProyectoLimpioCore.Helpers;
using ProyectoLimpioCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoLimpioCore
{
    public class Startup
    {
        IConfiguration Config;

        public Startup(IConfiguration config)
        {
            this.Config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<PathProvider>();

            String cadenasql = this.Config.GetConnectionString("casasql");
            String cadenamysql = this.Config.GetConnectionString("casamysql");
            services.AddTransient<IRepositoryCoches, RepositoryCochesSQL>();
            //services.AddTransient<IRepositoryCoches, RepositoryCochesXML>();
            //services.AddTransient<IRepositoryCoches, RepositoryCochesMYSQL>();
            services.AddDbContext<HospitalContext>(options => options.UseSqlServer(cadenasql));
            //services.AddDbContextPool<HospitalContext>(options => options.UseMySql(cadenamysql, ServerVersion.AutoDetect(cadenamysql)));
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default"
                    , pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}
