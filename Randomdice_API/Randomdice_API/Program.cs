using Microsoft.EntityFrameworkCore;
using Randomdice_API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddControllers();

// TODO:


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();