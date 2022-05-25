using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Contracts;
using Course.BLL.Services;
using Course.BLL.Services.Implementations;
using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Course.DAL.Repositories.Implementations;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CourseAPI.Extensions.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static object Configuration { get; private set; }

        public static void ConfigureCos(this IServiceCollection services)
        {
            // Allows requests from any source
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
            });

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
            {
                //Configure Password
                options.Password.RequireDigit = false; // Not mandatory digit
                options.Password.RequireLowercase = false; // Not mandatory lowercase
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3; // minimum password length
                options.Password.RequiredUniqueChars = 1; //

                // Configure Lockout - Lock user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // lock in 5minutes
                options.Lockout.MaxFailedAccessAttempts = 5; // failed 5 times => lock
                options.Lockout.AllowedForNewUsers = true;

                // Configure User.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email is unique

                // Configure Login.
                //Configure verify email (email existed is required)
                options.SignIn.RequireConfirmedEmail = false;
                // Verify phone number
                options.SignIn.RequireConfirmedPhoneNumber = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                //CVPANHTNT6-59
                options.UseSqlServer(configuration.GetConnectionString("MOCK_Course"), b =>
                b.MigrationsAssembly("CourseAPI"));
            });
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = configuration["AuthSettings:Audience"],
                    ValidIssuer = configuration["AuthSettings:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                };
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mock-BE1", Version = "v1" });
                c.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddScoped<ILoggerManager, LoggerManager>();
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<ICousesRepository, CousesRepository>();
            services.AddScoped<ISectionRepositoty, SectionRepositoty>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICourseReviewRepository, CourseReviewRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IAudioLanguageRepository, AudioLanguageRepository>();
            services.AddScoped<ICloseCaptionRepository, CloseCaptionRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();

        }
        /// <summary>
        /// Configure Services
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICourseReviewService, CourseReviewService>();

            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IAudioLanguageService, AudioLanguageService>();
            services.AddScoped<ICloseCaptionService, CloseCaptionService>();
            services.AddScoped<ILanguageService, LanguageService>();
        }
    }
}
