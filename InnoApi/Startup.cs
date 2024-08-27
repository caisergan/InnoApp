using InnoApi.Service;

namespace InnoApi
{
    public class Startup
    {
        /*
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // Register the PasswordHashingService for dependency injection
            services.AddSingleton<PasswordHashing>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }*/
    }

}
