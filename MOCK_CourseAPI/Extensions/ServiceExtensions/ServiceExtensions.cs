using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Contracts;
using Course.BLL.Services;
using Course.BLL.Services.Abstraction;
using Course.DAL.ConfigurationModels;
using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using CourseAPI.ActionFilters;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MOCK_Course.BLL.Services.Implementations;
using Newtonsoft.Json;
using Repository.Repositories;

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
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination");
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
                options.Password.RequireDigit = true; // Not mandatory digit
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

        public static void ConfigureInvalidFilter(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                //options.SuppressModelStateInvalidFilter = true;
            });

            services.AddScoped<ValidationFilterAttribute>();
            services.AddScoped<ValidationCourseExistAttribut>();
            services.AddScoped<ValidationDiscountExistAttribute>();
            services.AddScoped<ValidationCourseForDiscountExistAttribute>();
            services.AddScoped<ValidationDateTimeForDiscountAttribute>();
        }

        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {

            var jwtConfiguration = new JwtConfiguration();
            configuration.Bind(jwtConfiguration.Section, jwtConfiguration);

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfiguration.Issuer,
                    ValidAudience = jwtConfiguration.Audience,
                    IssuerSigningKey = new
                    SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Key))
                };
            });
        }

        public static void AddJwtConfiguration(this IServiceCollection services,
            IConfiguration configuration) =>
            services.Configure<JwtConfiguration>(configuration.GetSection("JwtSettings"));


        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Mock-BE2", Version = "v2" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
                c.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Bearer Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`. Admin Token: `eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjllNDdkYTY5LTNkM2UtNDI4ZC1hMzk1LWQ1MzkwODc1MzU4MiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJhZG1pbjEyMyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNjU1MDgyMzc3LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDM0MCIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzQwIn0.Y4zGPJon6XTp9C9NF88ptrJkDZW__FLogthprGPvqzA`",
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
            services.AddSingleton<ILoggerManager, LoggerManager>();
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ISectionRepositoty, SectionRepositoty>();
            services.AddScoped<ILectureRepository, LectureRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<ICourseReviewRepository, CourseReviewRepository>();
            services.AddScoped<ILectureRepository, LectureRepository>();
            services.AddScoped<IAudioLanguageRepository, AudioLanguageRepository>();
            services.AddScoped<ICloseCaptionRepository, CloseCaptionRepository>();
            //services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<ILevelRepository, LevelRepository>();
            services.AddScoped<ILevelRepository, LevelRepository>();

            services.AddScoped<ILectureRepository, LectureRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<ICourseCompletionRepository, CourseCompletionRepository>();
            services.AddScoped<ILectureCompletionRepository, LectureCompletionRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<ISavedCoursesRepository, SavedCoursesRepository>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuizRepository, QuizRepository>();
            services.AddScoped<IQuizOptionRepository, QuizOptionRepository>();
            services.AddScoped<IDipositRepository, DipositRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
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
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            //services.AddScoped<IUserService, UserService>();

            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<ILectureService, LectureService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<ICourseReviewService, CourseReviewService>();

            services.AddScoped<ILectureService, LectureService>();
            services.AddScoped<IAudioLanguageService, AudioLanguageService>();
            services.AddScoped<ICloseCaptionService, CloseCaptionService>();
            //services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<ILectureService, LectureService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<ICourseCompletionService, CourseCompletionService>();
            services.AddScoped<ILectureCompletionService, LectureCompletionService>();
            services.AddScoped<ILevelService, LevelService>();

            services.AddScoped<IUploadService, UploadService>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<ISavedCoursesService, SavedCoursesService>();
            services.AddScoped<IAssignmentService, AssignmentService>();
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IUploadFileService, UploadFileService>();
            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<IGoogleService, GoogleService>();
        }

        public static void ConfigureUpload(this IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = 837280000; // Limit on request body size
            });
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue; // Limit on individual form values
                x.MultipartBodyLengthLimit = int.MaxValue; // Limit on form body size
                x.MultipartHeadersLengthLimit = int.MaxValue; // Limit on form header size
            });
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = 837280000; // Limit on request body size
            });

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        }
    }
}
