using ESourcing.Sourcing.Data;
using ESourcing.Sourcing.Data.Interface;
using ESourcing.Sourcing.Repositories;
using ESourcing.Sourcing.Repositories.Interface;
using ESourcing.Sourcing.Settings;
using ESourcing.Sourcing.Settings.Interface;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

#region Swagger Dependencies

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ESourcing.Sourcing",
        Version = "v1"
    });
});
#endregion

builder.Services.AddControllers();
#region Configuration for Appsettings with dependency Injection
builder.Services.Configure<SourcingDatabaseSettings>(builder.Configuration.GetSection(nameof(SourcingDatabaseSettings)));
builder.Services.AddSingleton<ISourcingDatabaseSettings>(sp => sp.GetRequiredService<IOptions<SourcingDatabaseSettings>>().Value);
builder.Services.AddTransient<ISourcingContext, SourcingContext>();
builder.Services.AddTransient<IAuctionRepository, AuctionRepository>();
builder.Services.AddTransient<IBidRepository, BidRepository>();
#endregion

var app = builder.Build();

#region Swagger 
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sourcing API  V1");
});
#endregion
if (!app.Environment.IsDevelopment())
{
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


