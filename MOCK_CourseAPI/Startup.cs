using System.IO;
using Contracts;
using Course.BLL.Extensions;
using CourseAPI.Extensions.Middleware;
using CourseAPI.Extensions.ServiceExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;

namespace CourseAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            string configFileLog = string.Concat(Directory.GetCurrentDirectory(), "/nlog.config");
            LogManager.LoadConfiguration(configFileLog);

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

            services.AddControllers(config =>
            {
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

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v2/swagger.json", "MOCK_CourseAPI v2"));

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

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
        }
    }
}
