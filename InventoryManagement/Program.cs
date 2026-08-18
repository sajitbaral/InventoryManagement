using InventoryManagement.Data;
using InventoryManagement.IService;
using InventoryManagement.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("InventoryDb")
    ));

builder.Services.AddDbContext<OperationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("OperationDb")
    ));

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IStockService, StockService>();

builder.Services.AddScoped<IStockMovementService, StockMovementService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
