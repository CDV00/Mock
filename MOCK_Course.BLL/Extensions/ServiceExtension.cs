using Course.BLL.Services;
using Course.BLL.Services.Abstraction;
using Course.DAL.Repositories.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;

namespace Course.BLL.Extensions
{
    public class ServiceExtensions
    {
        /// <summary>
        /// Configure Repositories
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICousesRepository, CousesRepository>();
            services.AddScoped<ISectionRepositoty, SectionRepositoty>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<ICourseCompletionRepository, CourseCompletionRepository>();
            services.AddScoped<ILectureCompletionRepository, LectureCompletionRepository>();

            services.AddScoped<ILectureRepository, LectureRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICourseReviewRepository, CourseReviewRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<ISavedCoursesRepository, SavedCoursesRepository>();
        }
        /// <summary>
        /// Configure Services
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<ICourseService, CourseService>();
            //.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<ICourseCompletionService, CourseCompletionService>();
            services.AddScoped<ILectureCompletionService, LectureCompletionService>();
            services.AddScoped<ILectureService, LectureService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICourseReviewService, CourseReviewService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<ISavedCoursesService, SavedCoursesService>();

        }
    }
}
