using RandomDice_Login.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RandomDice_Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomDice_Login
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

            //// requires using Microsoft.Extensions.Options
            //services.Configure<BookstoreDatabaseSettings>(
            //    Configuration.GetSection(nameof(BookstoreDatabaseSettings)));

            //services.AddSingleton<IBookstoreDatabaseSettings>(sp =>
            //    sp.GetRequiredService<IOptions<BookstoreDatabaseSettings>>().Value);

            //// 1. appsettingjson에서 


            //services.AddSingleton<BookService>();

            // TODO: 서비스 등록.
            string connString = Configuration.GetConnectionString("DefaultConnection");
            services.Add(new ServiceDescriptor(typeof(BookService_Maira), new BookService_Maira(
                    connString
                )));
            services.AddControllers();
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
