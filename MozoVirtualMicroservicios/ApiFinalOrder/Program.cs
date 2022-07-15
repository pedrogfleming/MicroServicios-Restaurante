using ApiFinalOrder.ExternalServices;
using ApiFinalOrder.ExternalServices.IExternalServices;
using ApiFinalOrder.Infrastructure;
using ApiFinalOrder.Infrastructure.IRepository;
using ApiFinalOrder.Infrastructure.UnitOfWork;
using ApiFinalOrder.Integrations;
using ApiFinalOrder.Services;
using ApiFinalOrder.Services.IServices;
using MediatR;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("gateway_api", config =>
{
    config.BaseAddress = new Uri("https://localhost:7090");
});

builder.Services.AddDbContext<OrderDbContext>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddTransient<IMediator, Mediator>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
//Add HttpManager
builder.Services.AddTransient<IHttpManager, HttpManager>();
//Add Repositories
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductOrderRepository, ProductOrderRepository>();
//Add Services
builder.Services.AddTransient<IOrderServices, OrderServices>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductOrderService, ProductOrderService>();
//Add ExternalServices
builder.Services.AddTransient<IExternalServicesInventario, ExternalServicesInventario>();
builder.Services.AddTransient<IExternalServicesLogin, ExternalServicesLogin>();
builder.Services.AddTransient<IExternalServicesMesa, ExternalServicesMesa>();

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
