using System.IO;
using Contracts;
using Course.BLL.Extensions;
using Course.DAL.Models;
using CourseAPI.Extensions.Middleware;
using CourseAPI.Extensions.ServiceExtensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Web;

namespace CourseAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            //string configFileLog = string.Concat(Directory.GetCurrentDirectory(), "/nlog.config");
            //LogManager.LoadConfiguration(configFileLog);

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCos();
            services.ConfigureIISIntegration();
            services.ConfigureIdentity();
            services.ConfigureJwt(Configuration);
            services.AddJwtConfiguration(Configuration);
            services.ConfigureSwagger();
            services.ConfigureLoggerService();
            services.ConfigureServices();
            services.ConfigureRepositories();

            services.ConfigureSqlContext(Configuration);
            services.AddAutoMapper(typeof(MapperInitializer));
            services.ConfigureInvalidFilter();
            services.ConfigureUpload();

            services.AddSignalR();

            services.AddControllers(config =>
            {
            });
            //login Ex
            services.AddAuthentication()
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                })
                .AddGoogle(googleOptions =>
                {
                    // Đọc thông tin Authentication:Google từ appsettings.json
                    IConfigurationSection googleAuthNSection = Configuration.GetSection("Authentication:Google");
                    // Thiết lập ClientID và ClientSecret để truy cập API google
                    googleOptions.ClientId = googleAuthNSection["ClientId"];
                    googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];

                });
            services.AddAuthentication(
                options =>
                {
                    options.DefaultChallengeScheme = FacebookDefaults.AuthenticationScheme;
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                });

            
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.InjectStylesheet("/swagger-ui/custom.css");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "MOCK_CourseAPI v2");
            });

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<BroadcastHub>("/notify");
            });




        }
    }
}
