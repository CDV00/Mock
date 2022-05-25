using Course.BLL.Services;
using Course.BLL.Services.Implementations;
using Course.DAL.Repositories;
using Course.DAL.Repositories.Implementations;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<ILessonCompletionRepository, LessonCompletionRepository>();

            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICourseReviewRepository, CourseReviewRepository>();
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
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<ICourseCompletionService, CourseCompletionService>();
            services.AddScoped<ILessonCompletionService, LessonCompletionService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICourseReviewService, CourseReviewService>();
        }
    }
}
