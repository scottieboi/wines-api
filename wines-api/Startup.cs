using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WinesApi.Api.Region;
using WinesApi.Api.Vineyard;
using WinesApi.Api.Wine.CreateUpdateWine;
using WinesApi.Api.Wine.FindWine;
using WinesApi.Api.WineType;
using WinesApi.Models;

namespace WinesApi
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
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000").AllowAnyHeader();
                    });
            });

            services.AddControllers();

            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<IFindWineService, FindWineService>();
            services.AddScoped<IWineTypeService, WineTypeService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<IVineyardService, VineyardService>();
            services.AddScoped<ICreateUpdateWineService, CreateUpdateWineService>();
            services.AddScoped<IValidateWineRepository, ValidateWineRepository>();
            services.AddScoped<ICreateUpdateWineRepository, CreateUpdateWineRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();

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