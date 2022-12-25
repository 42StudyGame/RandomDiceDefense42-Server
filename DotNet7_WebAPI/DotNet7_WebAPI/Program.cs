using DotNet7_WebAPI.Model;
using DotNet7_WebAPI.Service;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using MySqlConnector;
using static Pipelines.Sockets.Unofficial.Threading.MutexSlim;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<DbConfig>(builder.Configuration.GetSection("DbConfig")); // Todo
builder.Services.AddTransient<IAccountDbService, MysqlService>();
builder.Services.AddSingleton<IActiveUserDbService, RedisActiveUserService>();
builder.Services.AddSingleton<ScenarioService>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<DotNet7_WebAPI.Middleware.AuthCheckMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
