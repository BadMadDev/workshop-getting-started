using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebSite.Data;

namespace WebSite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (Configuration.GetValue<bool>("USE_SQL_SERVER", false))
            {
                services.AddDbContext<ToDoListDbContext>(ConfigureSqlServer);
            }
            else
            {
                services.AddDbContext<ToDoListDbContext>(ConfigureSqlite);
            }

            services.AddControllersWithViews();
        }

        private void ConfigureSqlServer(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = "sqldb";
            builder.UserID = "sa";
            builder.Password = "P@ssw0rd123!";
            builder.InitialCatalog = "todo-db";
            optionsBuilder.UseSqlServer(builder.ConnectionString);
        }

        private void ConfigureSqlite(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "DataSource=ToDoList.db";

            Directory.CreateDirectory(@"/var/data");
            var builder = new SqliteConnectionStringBuilder(connectionString);
            builder.DataSource = Path.Combine(@"/var/data",
                builder.DataSource);
            optionsBuilder.UseSqlite(builder.ToString());
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
