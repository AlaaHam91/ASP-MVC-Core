using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BookStore.Models;
using BookStore.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BokStore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        //IConfiguration configuration;
        //public Startup(IConfiguration configuration)
        //{
        //    this.configuration = configuration;
        //}
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //  services.AddSingleton<IBookRepository<Book>,BookRepository>();
            //  services.AddSingleton<IBookRepository<Author>,AuthorRepository>();
            services.AddDbContext<BookStoreDbContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BookStoreDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            }

            );
          //  services.AddDbContext<BookStoreDbContext>();
          services.AddScoped<IBookRepository<Book>, BookDbRepository>();
            services.AddScoped<IBookRepository<Author>, AuthorDBRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            app.UseStaticFiles();

               app.UseMvcWithDefaultRoute();
            //app.UseMvc(route=> {
            //    route.MapRoute("default","{controller=Book}/{action=Index}/{Id?}");
            //});
        }
    }
}