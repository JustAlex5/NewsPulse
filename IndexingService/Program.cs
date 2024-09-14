using CommonExtensions.Dal;
using IndexingService;
using MySqlConnector.Logging;
using CommonExtensions.Interfaces;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped < IMysqlDal, MySqlDal>();

var host = builder.Build();
host.Run();
