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
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICousesRepository, CousesRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

        }
        /// <summary>
        /// Configure Services
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<ICourseService, CourseService>();
           //services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
        }
    }
}
