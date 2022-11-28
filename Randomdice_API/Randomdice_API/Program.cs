using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;
using Randomdice_API;
using Randomdice_API.Configurations;
using Randomdice_API.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<MySqlConnection>(_ => new MySqlConnection(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddTransient<UserService>();
builder.Services.AddSingleton<RedisService>();
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
builder.Services.AddAuthentication(options =>
{
    // JwtBearerDefaults : Microsoft.AspNetCore.Authentication.JwtBearer 6.xx버전으로 너겟 설치
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    // jwt에 쓰일 키를 받음.
    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value);

    // Defines whether the bearer token should be stored in the
    // <see cref="AuthenticationProperties"/> after a successful authorization.
    jwt.SaveToken = true;
    // Gets or sets the parameters used to validate identity tokens.
    jwt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        // Gets or sets a boolean that controls if validation of the <see cref="SecurityKey"/> that signed the securityToken is called.
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false, //for Dev.
        ValidateAudience = false, //for dev.
        RequireExpirationTime = false, //for dev, needs to be updated when refresh Token id added.
        ValidateLifetime = true
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();