using ApiInventario.Integrations;
using ApiInventario.ExternalServices;
using ApiInventario.ExternalServices.IExternalServices;
using ApiInventario.Infrastructure;
using ApiInventario.Infrastructure.IRepository;
using ApiInventario.Infrastructure.UnitOfWork;
using ApiInventario.Services;
using ApiInventario.Services.IService;
using MediatR;
using Serilog;

try
{
    var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration) 
        .CreateLogger();

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddHttpClient("gateway_api", config =>
    {
        config.BaseAddress = new Uri("https://localhost:7090");
    });

    // Add services to the container.
    builder.Services.AddDbContext<InventarioDbContext>();
    builder.Services.AddMediatR(typeof(Program));
    builder.Services.AddTransient<IMediator, Mediator>();
    builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
    builder.Services.AddTransient<IProductRepository, ProductRepository>();
    builder.Services.AddTransient<IProductService, ProductService>();
    builder.Services.AddTransient<IExternalServicesLogin, ExternalServicesLogin>();    
    builder.Services.AddTransient<IHttpManager, HttpManager>();
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(opt =>
    {
        opt.CustomSchemaIds(type => type.ToString());
    });

    builder.Host.UseSerilog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled Exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}