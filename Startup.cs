using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseApiEF.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace BaseApiEF
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
            services.AddDbContext<DataContext>(OperatingSystem => OperatingSystem.UseInMemoryDatabase("BaseApiEF"));
            services.AddScoped<DataContext, DataContext>();

            services
                 .AddSwaggerGen(c =>
                 {
                     c.SwaggerDoc("1.0.0", new OpenApiInfo
                     {
                         Version = "1.0.0",
                         Title = "Base Api Ado",
                         Description = "Base Api Ado (ASP.NET Core 3.1)",
                         Contact = new OpenApiContact()
                         {
                             Name = "Mateus Andrade",
                             Url = new Uri("https://github.com/msandradenet"),
                             Email = "mateus_andrade1994@yahoo.com.br"
                         }
                     });
                     c.CustomSchemaIds(type => type.FullName);
                 });

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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/1.0.0/swagger.json", "Swagger");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
