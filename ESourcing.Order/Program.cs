using AutoMapper;
using ESourcing.Order.Consumers;
using ESourcing.Order.Extensions;
using ESourcing.Order.Mapping;
using EventBusRabbitMQ;
using Microsoft.OpenApi.Models;
using Ordering.Application;
using Ordering.Infrastructure;
using RabbitMQ.Client;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Host.UseDefaultServiceProvider(s => s.ValidateScopes =false);

builder.Services.AddAutoMapper(typeof(Program).Assembly);
#region EventBus RabbitMQ
builder.Services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
    var factory = new ConnectionFactory()
    {
        HostName = builder.Configuration["EventBus:HostName"]
    };
    if (!string.IsNullOrWhiteSpace(builder.Configuration["EventBus:UserName"]))
        factory.UserName = builder.Configuration["EventBus:UserName"];
    if (!string.IsNullOrWhiteSpace(builder.Configuration["EventBus:Password"]))
        factory.UserName = builder.Configuration["EventBus:Password"];
    var retryCount = 5;
    if (!string.IsNullOrWhiteSpace(builder.Configuration["EventBus:RetryCount"]))
        retryCount = Convert.ToInt32(builder.Configuration["EventBus:RetryCount"]);
    return new DefaultRabbitMQPersistentConnection(factory, retryCount, logger);
});
builder.Services.AddSingleton<EventBusOrderCreateConsumer>();
#endregion

#region Swagger Dependencies

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Order API",
        Version = "v1"
    });
});
#endregion

var app = builder.Build();
app.UseRouting();
app.UseAuthorization();
app.MigrateDatabase();
#region Swagger 
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API  V1");
});
#endregion
app.UseRabbitListener();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.Run();
