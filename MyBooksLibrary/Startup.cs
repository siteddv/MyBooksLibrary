using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MyBooksLibrary.Data;
using MyBooksLibrary.Data.Services;
using MyBooksLibrary.Exceptions;


namespace MyBooksLibrary
{
    public class Startup
    {
        public string ConntectionString { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConntectionString = Configuration.GetConnectionString("DefaultConnectionString");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConntectionString));

            services.AddTransient<BookService>();
            services.AddTransient<AuthorsService>();
            services.AddTransient<PublishersService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyBooksLibrary", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBooksLibrary v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.ConfigureBuildInExceptionHandler(loggerFactory);
            /*app.ConfigureCustomExceptionHandler();*/

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            AddDbInitializer.Seed(app);
        }
    }
}
