using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Organic.MessageBus;
using Organic.Services.ShoppingCartAPI;
using Organic.Services.ShoppingCartAPI.Extensions;
using ShoppingCartAPI.Data;
using ShoppingCartAPI.Service;
using ShoppingCartAPI.Service.IService;
using ShoppingCartAPI.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IMessageBus, MessageBus>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<BackendApiAuthenticationHttpClientHandler>();
builder.Services.AddHttpClient("Product", u => u.BaseAddress =
new Uri(builder.Configuration["ServiceUrls:ProductAPI"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>(); ;
builder.Services.AddHttpClient("Coupon", u => u.BaseAddress =
new Uri(builder.Configuration["ServiceUrls:CouponAPI"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>(); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
	option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});
	option.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference= new OpenApiReference
				{
					Type=ReferenceType.SecurityScheme,
					Id=JwtBearerDefaults.AuthenticationScheme
				}
			}, new string[]{}
		}
	});
});
builder.AddAppAuthentication();

builder.Services.AddAuthorization();
IMapper mapper = MapperConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<AppDbContext>(option => {
	option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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
