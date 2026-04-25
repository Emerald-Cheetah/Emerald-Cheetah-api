using Emerald.Cheetah.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Emerald.Cheetah.Api.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

string authority = builder.Configuration["Auth0:Authority"] ?? 
    throw new ArgumentException("Auth0:Authority");
string audience = builder.Configuration["Auth0:Audience"] ??
    throw new ArgumentException("Auth0:Audience");

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Authority = authority;
        options.Audience = audience;
    });

builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("delete:catalog", policy => 
            policy.RequireAuthenticatedUser().RequireClaim("scope", "delete:catalog"));
    });

builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlite("Data Source=./Registrar.sqlite",
        b => b.MigrationsAssembly("Emerald.Cheetah.Api")));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();