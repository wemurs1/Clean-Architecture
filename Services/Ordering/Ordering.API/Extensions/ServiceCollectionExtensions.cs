using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Abstractions;
using Ordering.Application.Behaviours;
using Ordering.Application.Orders.CreateOrder;
using Ordering.Application.Orders.DeleteOrder;
using Ordering.Application.Orders.GetOrders;
using Ordering.Application.Orders.UpdateOrder;
using Ordering.Application.Validators;
using Ordering.Core.Repositories;
using Ordering.Infra.Data;
using Ordering.Infra.Repositories;

namespace Ordering.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOrderingServices(this IServiceCollection services, IConfiguration configuration) 
        {
            //1 Database
            services.AddDbContext<OrderContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("OrderingConnectionString"),
                    sqloptions =>
                    {
                        sqloptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null);

                        // THIS IS THE FIX
                        sqloptions.MigrationsAssembly("Ordering.Infra");
                    });
            });

            //services.AddOrderingInfrastructure(configuration);

            //2 Repositories
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();

            //3 CQRS
            services.Scan(scan => scan
            .FromAssemblies(typeof(ICommandHandler<>).Assembly)

            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            services.AddScoped<CreateOrderCommandHandler>();
            services.AddScoped<UpdateOrderCommandHandler>();
            services.AddScoped<DeleteOrderCommandHandler>();
            services.AddScoped<GetOrderListQueryHandler>();

            //4 Fluent Validation
            services.AddValidatorsFromAssembly(typeof(CreateOrderCommandValidator).Assembly);

            //5 Decorators Pipeline
            services.Decorate(
                typeof(ICommandHandler<,>),
                typeof(ValidationCommandHandlerDecorator<,>));

            services.Decorate(
                typeof(ICommandHandler<,>),
                typeof(UnhandledExceptionCommandHandlerDecorator<,>));
            return services;
        }
    }
}