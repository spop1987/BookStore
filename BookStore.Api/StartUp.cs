using BookStore.DataStore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api
{
    public class StartUp
    {
        public IConfiguration _configuration {get;}
        public StartUp(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddDependencyServices();
            ConfigureDb(services);
            services.AddEndpointsApiExplorer();
        }

        protected virtual void ConfigureDb(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>(options => {
                options.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseRouting();
            app.UseEndpoints(endpoints => 
            {
                endpoints.MapControllers();
            });
            MigrateDb(app);
        }

        protected virtual void MigrateDb(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BookStoreContext>();
                context?.Database.Migrate();
            }
        }
    }
}