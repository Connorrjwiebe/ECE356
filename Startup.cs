
using Microsoft.EntityFrameworkCore;
using Supermarket.API.Controllers.Config;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Services;
using Supermarket.API.Extensions;
using Supermarket.API.Persistence.Contexts;
using Supermarket.API.Persistence.Repositories;
using Supermarket.API.Services;

namespace Supermarket.API
{
    
    public class Startup
    {
        
        private readonly IConfiguration Configuration;
        private readonly  string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration) => Configuration = configuration;
        
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddMemoryCache();

            services.AddCustomSwagger();
            

    services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:7108/api.categories",
                                              "https://localhost:44456");
                      });
});

			services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

			services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                // Adds a custom error response factory when ModelState is invalid
                options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ProduceErrorResponse;
            });

            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionstring = Configuration.GetConnectionString("DefaultConnection");
                //options.UseInMemoryDatabase(Configuration.GetConnectionString("memory") ?? "data-in-memory");
                options.UseMySQL(connectionstring);

            });

            services.AddScoped<ISensorRepository, SensorRepository>();
            //services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ISensorService, SensorService>();
            //services.AddScoped<IProductService, ProductService>();

            services.AddAutoMapper(typeof(Startup));
        }

        

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomSwagger();
            

            app.UseCors(MyAllowSpecificOrigins);
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}