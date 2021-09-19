using Company.Schedule.Api.Middleware;
using Company.Schedule.Domain.Interfaces;
using Company.Schedule.Infrastructure;
using Company.Schedule.Services;
using Company.Schedule.Services.Domain;
using Company.Schedule.Services.Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text.Json;

namespace Company.Schedule.Api
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

            services.AddControllers()
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                }); 
            services
                .AddCors(o => o.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin(); //obviously not production ready approach
                    }))
                .AddDbContext<CompanyScheduleContext>(c => c.UseSqlServer(Configuration.GetConnectionString("DBConnection")))
                .AddTransient<IRepository, Repository>()
                .AddTransient<ICompanyScheduleService, CompanyScheduleService>()
                .AddTransient<IDateCalculator, DateCalculator>()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Company.Schedule.Api", Version = "v1" });
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company.Schedule.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            app.UseMiddleware<LoggerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
