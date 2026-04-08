using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapGet("/unsecured", () => "Stormy");
app.MapGet("/secured", () => "Sunny").RequireAuthorization();
app.MapGet("/anonymous", () => "Cloudy").AllowAnonymous();

app.Run();


#if DEBUG
// Enable test access
public partial class Program { }
#endif
