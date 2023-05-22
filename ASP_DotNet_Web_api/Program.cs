using ASP_DotNet_Web_api.Data;
using ASP_DotNet_Web_api.Mapping;
using ASP_DotNet_Web_api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<MZWalksDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MZWalksConnectionString")));





//builder.Services.AddScoped<IRegionRepository, InMemmoryRegionRepository>();   //-> if we use InMemmory dataBase 
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();



builder.Services.AddAutoMapper(typeof(AutoMapperProfiles)); //find mapping in that file when programme starts. 


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
