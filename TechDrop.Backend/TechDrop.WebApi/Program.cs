using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TechDrop.Data;
using TechDrop.Logic;
using TechDrop.Logic.Configurations;
using TechDrop.Logic.Services;
using TechDrop.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Добавляется сваггер с настройками для Аутентификации.
// TODO Перенести настройки в отдельный файл. Мб в AuthSettings или ещё куда
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Авторизация осуществляется c помощью JwtToken в заголовке Authorization.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// Регистрируем настройки и сервис аутентификации
builder.Services.AddTransient<AuthSettings>();
builder.Services.AddTransient<AuthService>();
// Регистрируем аксессор контекста запроса
builder.Services.AddHttpContextAccessor();
// Регистрируем сервис для доступа к пользователю
builder.Services.AddTransient<UserService>();

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
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = authSettings.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = authSettings.GetSymmetricSecurityKey(),
            ValidateLifetime = false
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
app.UseAuthorization();

app.MapControllers();

app.Run();