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

app.MapGet("/unsecured", () => "Stormy");
app.MapGet("/secured", () => "Sunny").RequireAuthorization();
app.MapGet("/anonymous", () => "Cloudy").AllowAnonymous();

app.Run();


#if DEBUG
// Enable test access
public partial class Program { }
#endif
