using System.Reflection;
using Basket.Application.GrpcService;
using Basket.Application.Handlers;
using Basket.Application.Settings;
using Basket.Core.Repositories;
using Basket.Infra.Repositories;
using Basket.Infra.Settings;
using Discount.Grpc.Protos;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(CreateShoppingCartCommandHandler).Assembly
};
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(assemblies);
});
builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection(nameof(CacheSettings)));
builder.Services.Configure<GrpcSettings>(builder.Configuration.GetSection(nameof(GrpcSettings)));
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>((sp, config) =>
{
    var grpcSetting = sp.GetRequiredService<IOptions<GrpcSettings>>().Value;
    config.Address = new Uri(grpcSetting.DiscountUrl);
});
builder.Services.AddScoped<DiscountGrpcService>();

builder.Services.AddStackExchangeRedisCache((options) =>
{
    options.Configuration = builder.Configuration[$"{nameof(CacheSettings)}:ConnectionString"];
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
