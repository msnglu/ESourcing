using ESourcing.Order.Extensions;
using Ordering.Infrastructure;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
app.MigrateDatabase();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

}

app.Run();
