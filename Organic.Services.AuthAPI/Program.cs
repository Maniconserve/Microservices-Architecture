using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Organic.Services.AuthAPI.Data;
using Organic.Services.AuthAPI.Models;
using Organic.Services.AuthAPI.Services.IService;
using Organic.Services.AuthAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.AddDbContext<AppDbContext>(option => {
	option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
	.AddDefaultTokenProviders();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();	
app.UseAuthorization();

app.MapControllers();

app.Run();
