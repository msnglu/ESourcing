using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((context,config) =>
{
    config.AddJsonFile("ocelot.json");

});
builder.Services.AddOcelot();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseOcelot();
app.UseRouting();
app.Run();
