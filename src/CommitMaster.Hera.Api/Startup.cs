using Microsoft.EntityFrameworkCore;
using CommitMaster.Hera.Infra.Data;
using Microsoft.OpenApi.Models;

namespace CommitMaster.Sirius.Admin
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
  
            
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CommitMaster.Hera.Api", Version = "v1" });
            });

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
