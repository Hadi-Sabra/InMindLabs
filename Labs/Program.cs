using Lab1.Middlewares;
using Lab1.Services;
using Lab1.Filters;
using AutoMapper;
using Lab1.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ObjectMapperService>();
builder.Services.AddScoped<LoggingActionFilter>();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

// dotnet ef dbcontext scaffold “Host=localhost;Port=5433;Database=postgres;Username=hadi;Password=123" Npgsql.EntityFrameworkCore.PostgreSQL -o Models 

builder.Services.AddDbContext<LibrarydbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Host=localhost;Port=5433;Database=postgres;Username=hadi;Password=123")));

builder.Services.AddControllers()
    .AddOData(opt => opt.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100));

builder.Services.AddControllers();

/*
To add the filter to the whole controller: 
builder.Services.AddControllers(options =>
{
    options.Filters.Add<LoggingActionFilter>();
});
*/

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

//Custom middlewares are inserted lastly 
app.UseRequestLogging();

app.MapControllers();

app.Run();