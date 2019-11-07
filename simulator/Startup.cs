namespace Simulator
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using x3e;
    using x3e.components;

    public class Startup
    {
        public Startup(IConfiguration configuration) 
            => Configuration = configuration;
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton(x => Simulator<EtherRod>.Setup<EtherRod>(1));
            services.AddHostedService<SimulatorBridge>();
            services.AddMvc();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllersWithViews();
            services.Configure<RazorViewEngineOptions>(z => z.ViewLocationExpanders.Add(new FolderLocationExpander()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) 
                app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Web}/{action=Index}/{id?}");
            });
        }
    }
}