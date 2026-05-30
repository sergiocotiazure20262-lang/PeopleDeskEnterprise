using Microsoft.EntityFrameworkCore;
using PeopleDesk.Domain.Interfaces.Repositories;
using PeopleDesk.Domain.Interfaces.Services;
using PeopleDesk.Domain.Services;
using PeopleDesk.Infrastructure.Contexts;
using PeopleDesk.Infrastructure.Repositories;

namespace PeopleDesk.Api.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //DbContext
            services.AddDbContext<PeopleDeskDbContext>(options =>
            {
                //ConnectionString
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //Repositórios
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IChamadoRepository, ChamadoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Serviços de dominio
            services.AddScoped<IChamadoDomainService, ChamadoDomainService>();

            return services;
        }
    }
}
