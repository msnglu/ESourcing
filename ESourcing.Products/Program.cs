using ESourcing.Products.Data;
using ESourcing.Products.Data.Interfaces;
using ESourcing.Products.Repositories;
using ESourcing.Products.Repositories.Interfaces;
using ESourcing.Products.Settings;
using ESourcing.Products.Settings.Interface;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

#region Swagger Dependencies

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ESourcing.Products",
        Version = "v1"
    });
});
#endregion
#region Configuration Dependencies
builder.Services.Configure<ProductDatabaseSettings>(builder.Configuration.GetSection(nameof(ProductDatabaseSettings)));
builder.Services.AddSingleton<IProductDatabaseSettings>(sp => sp.GetRequiredService<IOptions<ProductDatabaseSettings>>().Value);
#endregion
#region Project Dependencies
builder.Services.AddTransient<IProductContext, ProductContext>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
#endregion
builder.Services.AddControllers();
var app = builder.Build();
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ESourcing.Products V1");
});
if (!app.Environment.IsDevelopment())
{
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

