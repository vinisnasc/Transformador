using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Transformador.API.Configurations;
using Transformador.CrossCutting;
using Transformador.CrossCutting.Mapper;
using Transformador.Data.MongoConfiguration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<IMongoDbSettings>(serviceProvider =>
    serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddAutoMapperConfiguration();
builder.Services.RegisterServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseSwaggerConfiguration(app.Environment);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();