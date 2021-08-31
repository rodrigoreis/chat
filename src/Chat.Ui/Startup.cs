using Chat.Repositories.DependencyInjections;
using Chat.Ui.Hub;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Chat.Ui
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddRouting(options =>
                    {
                        options.LowercaseUrls = true;
                        options.LowercaseQueryStrings = true;
                    }
                )
                .AddRazorPages()
                .AddRazorRuntimeCompilation();

            services.AddRepositories();
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints
            (
                endpoints =>
                {
                    endpoints.MapRazorPages();
                    endpoints.MapHub<ChatHub>("/chat");
                }
            );
        }
    }
}