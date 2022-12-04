using DotNet7_WebAPI.Model;
using DotNet7_WebAPI.Service;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using MySqlConnector;
using static Pipelines.Sockets.Unofficial.Threading.MutexSlim;

var builder = WebApplication.CreateBuilder(args);
//var builder = WebApplication.CreateBuilder(new WebApplicationOptions
//{
//    ContentRootPath = Directory.GetCurrentDirectory(),
//});
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
//app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(
//           Path.Combine(builder.Environment.ContentRootPath, "Scenarios")),
//    RequestPath = "/Scenarioss"
//});
app.UseMiddleware<DotNet7_WebAPI.Middleware.AuthCheckMiddleware>();
app.UseHttpsRedirection();
//app.UseStaticFiles();
app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Scenarios")),
    RequestPath = "/Scenarios",
    EnableDefaultFiles= true
});

app.UseAuthorization();

app.MapControllers();

app.Run();
