using MySqlConnector;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddControllers();
builder.Services.AddTransient<MySqlConnection>(_ => new MySqlConnection(System.Environment.GetEnvironmentVariable("CONNECTION_STRING")));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();