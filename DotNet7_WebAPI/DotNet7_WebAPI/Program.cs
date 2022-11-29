using DotNet7_WebAPI.Model;
using DotNet7_WebAPI.Service;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using static Pipelines.Sockets.Unofficial.Threading.MutexSlim;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<DbConfig>(builder.Configuration.GetSection("DbConfig")); // Todo
builder.Services.AddTransient<MysqlService>();
builder.Services.AddSingleton<RedisService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
