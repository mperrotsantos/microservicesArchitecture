using AutoMapper;
using GeekShopping.ProductAPI.Mapper;
using GeekShopping.ProductAPI.Model.Context;
using GeekShopping.ProductAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

var connection = builder.Configuration["MySQlConnection:MySQlConnectionString"];
// Add services to the container.
builder.Services.AddDbContext<MySqlContext>(options => options.
                                                        UseMySql(connection, 
                                                                 new MySqlServerVersion(new Version(8,0,33))));
//Mapper
IMapper mapper = MappingConfig.ResgisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


//Repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
