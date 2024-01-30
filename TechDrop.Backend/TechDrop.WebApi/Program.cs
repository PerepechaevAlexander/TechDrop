using Microsoft.EntityFrameworkCore;
using TechDrop.Data;
using TechDrop.Logic;
using TechDrop.WebApi.Extensions;
using TechDrop.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TechDropDbContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb"));
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<LogicPointer>());

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

app.UseAuthorization();

app.UseMiddleware<AuthMiddleware>();

app.MapControllers();

app.Run();