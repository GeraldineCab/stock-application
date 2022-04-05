using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StockApplication.Business.Services.Interfaces;
using StockApplication.Persistence;

namespace StockApplication.Bot
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StockApplication.Bot", Version = "v1" });
            });

            ConfigureDependencies(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StockApplication.Bot v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureDependencies(IServiceCollection services)
        {
            services.AddTransient<HttpClient>();
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<StockApplicationContext>();

            services.Scan(scan => scan
                .FromAssemblies(typeof(IStockService).Assembly)
                .AddClasses()
                .AsMatchingInterface().WithTransientLifetime()
                .FromAssemblies(typeof(IStockApplicationContext).Assembly)
                .AddClasses().AsMatchingInterface().WithSingletonLifetime());
        }
    }
}
