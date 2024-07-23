using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StudentsAdminPortal.API.Exceptions;
using StudentsAdminPortal.API.Models;
using StudentsAdminPortal.API.ServiceClass;
using StudentsAdminPortal.Repository;
using System.Reflection;
using System.Text.Json.Serialization;

namespace StudentsAdminPortal.API
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
            services.AddDbContext<StudentsDbContext>(
               options => options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Students_Univ1;Integrated Security=True"));
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IStudentServiceClass, StudentServiceClass>();
            services.AddControllers();
            services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition
            = JsonIgnoreCondition.WhenWritingNull);
            services.AddControllers().AddFluentValidation(v =>
            {
                v.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentsAdminPortal.API", Version = "v1" });
            });
            services.AddAutoMapper(typeof(Startup).Assembly);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
               app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentsAdminPortal.API v1"));
            }

            app.UseMiddleware<GlobalExceptionHandler>();
            app.UseCors( x => x.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            
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
