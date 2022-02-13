using CommitMaster.Hera.Infra.Data;
using CommitMaster.Sirius.Api.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CommitMaster.Hera.Api
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

            services.AddJwtAuthorization(Configuration);
            services.AddSwaggerWithSecurity();

            services.AddDbContext<MyAppContext>(options =>
            {
                options.UseNpgsql(Configuration["ConnectionStrings:PostgreSqlConnection"],
                    b => b.MigrationsAssembly("CommitMaster.Hera.Api"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CommitMaster.Sirius.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCustomAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
