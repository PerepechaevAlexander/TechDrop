using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TechDrop.Data;
using TechDrop.Logic;
using TechDrop.Logic.Configurations;
using TechDrop.Logic.Services;
using TechDrop.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрируем настройки и сервис аутентификации
builder.Services.AddTransient<AuthSettings>();
builder.Services.AddTransient<AuthService>();

// Регистрируем контекст БД
builder.Services.AddDbContext<TechDropDbContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb"));
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<LogicPointer>());


// Добавляем аутентификацию на основе JWT-токенов
var authSettings = new AuthSettings(builder.Configuration);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = authSettings.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = authSettings.GetSymmetricSecurityKey()
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigins",
        policyBuilder =>
        {
            policyBuilder
                .AllowCredentials()
                .AllowAnyHeader()
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyMethod()
                .WithOrigins(
                    "http://localhost:4200"
                );
        });
});




var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowMyOrigins");

app.UseHttpsRedirection();

app.ConfigureExceptionHandler();

app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();