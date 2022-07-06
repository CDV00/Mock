using Contracts;
using Course.BLL.Extensions;
using Course.DAL.Models;
using CourseAPI.Controllers;
using CourseAPI.Extensions.Middleware;
using CourseAPI.Extensions.ServiceExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
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
            services.ConfigureAuthentication(Configuration);
            services.AddCustomMediaTypes();
            //services.ConfigureHttpCacheHeaders();
            //services.ConfigureResponseCaching();

            services.AddSignalR();

            services.AddControllers(config =>
            {
                //config.ReturnHttpNotAcceptable = true;
                //config.CacheProfiles.Add("120SecondsDuration", new CacheProfile
                //{
                //    Duration = 120
                //});
            }).AddNewtonsoftJson();

            //login Ex
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManagerService logger)
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

            //app.UseResponseCaching();
            //app.UseHttpCacheHeaders();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHub<Course.BLL.Services.Hubs.NotificationHub>("notificationHub");
            });
        }
    }
}
