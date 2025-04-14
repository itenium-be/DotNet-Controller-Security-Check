var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", () => "Stormy")
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.MapGet("/secured-weatherforecast", () => "Sunny")
    .WithName("GetSecuredWeatherForecast")
    .RequireAuthorization()
    .WithOpenApi();

app.MapGet("/anonymous-weatherforecast", () => "Cloudy")
    .WithName("GetSecuredWeatherForecast")
    .AllowAnonymous()
    .WithOpenApi();

app.Run();


#if DEBUG
public partial class Program { }  // Enable test access
#endif
