using CacheService.Dal;
using CacheService.Interfaces;
using CacheService.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
//});

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("RedisConnection"), true);
    configuration.AllowAdmin = true; // Enable admin commands
    return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddHostedService<CacheInit>();

builder.Services.AddSingleton<IMetaData,MetaData>();
builder.Services.AddSingleton<IRedisCache, RedisCache>();






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
