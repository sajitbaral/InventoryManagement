using Microsoft.EntityFrameworkCore;
using Operational.Application.Interfaces;
using Operational.Application.Services;
using Operational.Infrastructure.Clients;
using Operational.Infrastructure.Persistence;
using Operational.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();



builder.Services.AddHttpClient<IInventoryClient, InventoryApiClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5180");
    client.Timeout = TimeSpan.FromSeconds(30);
});


builder.Services.AddDbContext<OperationalDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("OperationalDb")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
