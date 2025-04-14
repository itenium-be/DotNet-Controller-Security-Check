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

app.MapGet("/unsecured", () => "Stormy")
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.MapGet("/secured", () => "Sunny")
    .WithName("GetSecuredWeatherForecast")
    .RequireAuthorization()
    .WithOpenApi();

app.MapGet("/anonymous", () => "Cloudy")
    .WithName("GetSecuredWeatherForecast")
    .AllowAnonymous()
    .WithOpenApi();

app.Run();


#if DEBUG
// Enable test access
public partial class Program { }
#endif
