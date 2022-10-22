using MySqlConnector;
using Randomdice_API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddControllers();

builder.Services.AddTransient<MySqlConnection>(_ => new MySqlConnection(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddTransient<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();