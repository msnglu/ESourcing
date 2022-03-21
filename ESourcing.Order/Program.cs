using ESourcing.Order.Extensions;
using Ordering.Application;
using Ordering.Infrastructure;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddControllers();


var app = builder.Build();
app.MigrateDatabase();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

}

app.Run();
