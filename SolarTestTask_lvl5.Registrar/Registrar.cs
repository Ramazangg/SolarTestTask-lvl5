using Board.Infrastucture.DataAccess;
using Board.Infrastucture.DataAccess.Interfaces;
using Board.Infrastucture.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SolarTestTask_lvl5.AppData.Contexts;
using SolarTestTask_lvl5.AppData.Contexts.Mail;
using SolarTestTask_lvl5.DataAccess.Contexts;

namespace SolarTestTask_lvl5.Registrar
{
    public static class Registrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IDbContextOptionsConfigurator<BoardDbContext>, BoardDbContextConfiguration>();

            services.AddDbContext<BoardDbContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
                ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<BoardDbContext>>()
                .Configure((DbContextOptionsBuilder<BoardDbContext>)dbOptions)));

            services.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<BoardDbContext>()));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMailService, MailService>();

            return services;
        }
    }
}
