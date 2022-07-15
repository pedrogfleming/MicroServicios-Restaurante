using ApiMesa.ExternalServices;
using ApiMesa.ExternalServices.IExternalServices;
using ApiMesa.Infrastructure;
using ApiMesa.Infrastructure.IRepository;
using ApiMesa.Infrastructure.UnitOfWork;
using ApiMesa.Integrations;
using ApiMesa.Services;
using ApiMesa.Services.IServices;
using MediatR;

var configuration = new ConfigurationBuilder()
.AddJsonFile("appsettings.json")
.Build();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddHttpClient("gateway_api", config =>
{
    config.BaseAddress = new Uri("https://localhost:7090");
});
builder.Services.AddControllers();
builder.Services.AddDbContext<ApiMesaContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddTransient<IMediator, Mediator>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IMesaRepository, MesaRepository>();
builder.Services.AddTransient<IMesaServices, MesaServices>();
builder.Services.AddTransient<IOrder_MesaRepository, Order_MesaRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IExternalServicesLogin, ExternalServicesLogin>();
builder.Services.AddTransient<IHttpManager, HttpManager>();
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
