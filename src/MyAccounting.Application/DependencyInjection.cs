using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MyAccounting.Application.Services;

namespace MyAccounting.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            services.AddScoped<ITransactionService, TransactionService>();

            return services;
        }
    }
}