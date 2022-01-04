using ESourcing.Products.Data;
using ESourcing.Products.Data.Interfaces;
using ESourcing.Products.Repositories;
using ESourcing.Products.Repositories.Interfaces;
using ESourcing.Products.Settings;
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

builder.Services.AddControllers();
ConfigureConfiguration(builder.Configuration);
ConfigureServices(builder.Services);
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

ConfigureMiddleware(app, app.Services);
ConfigureEndpoints(app, app.Services);
app.Run();
void ConfigureConfiguration(ConfigurationManager configuration) { }
void ConfigureServices(IServiceCollection services)  {
    #region Configuration Dependencies
    services.Configure<ProductDatabaseSettings>(builder.Configuration.GetSection(nameof(ProductDatabaseSettings)));
    services.AddSingleton<IProductDatabaseSettings>(sp => sp.GetRequiredService<IOptions<ProductDatabaseSettings>>().Value);
    #endregion

    #region Project Dependencies
    services.AddTransient<IProductContext, ProductContext>();
    services.AddTransient<IProductRepository, ProductRepository>();
    #endregion  
}
void ConfigureMiddleware(IApplicationBuilder app, IServiceProvider services)  { }
void ConfigureEndpoints(IEndpointRouteBuilder app, IServiceProvider services)  { }
