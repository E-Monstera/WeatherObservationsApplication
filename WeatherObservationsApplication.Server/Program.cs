using WeatherObservationsApplication.Server.Interfaces;
using WeatherObservationsApplication.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Register services
builder.Services.AddScoped<IWeatherService, WeatherService>();


var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.MapFallbackToFile("/index.html");

app.Run();
