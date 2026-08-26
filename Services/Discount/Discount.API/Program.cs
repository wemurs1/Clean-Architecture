using System.Reflection;
using Discount.API.Services;
using Discount.Application.Handlers;
using Discount.Core.Repositories;
using Discount.Infra.Repositories;
using Discount.Infra.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(CreateDiscountCommandHandler).Assembly
};
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(assemblies);
});

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddGrpc();

var app = builder.Build();

app.MigrateDatabase();
app.UseRouting();
app.MapGrpcService<DiscountService>();
// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapGrpcService<DiscountService>();
// });

app.Run();
