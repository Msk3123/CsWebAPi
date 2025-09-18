using CompanyEmployees.Extensions;
using CompanyEmployees;
using Contracts.Interfaces;
using NLog;
using NLog.Web;

// Early init of NLog to allow startup and exception logging, before host is built
var logger = LogManager.Setup()
    .LoadConfigurationFromFile("nlog.config")
    .GetCurrentClassLogger();

try
{
    logger.Debug("Starting application");

    var builder = WebApplication.CreateBuilder(args);

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add services to the container.
    builder.Services.ConfigureCors();
    builder.Services.ConfigureIISIntegration();
    builder.Services.ConfigureLoggerService();
    builder.Services.ConfigureSqlContext(builder.Configuration);
    builder.Services.ConfigureRepositoryManager();
    builder.Services.AddAutoMapper(cfg => { }, typeof(Program).Assembly);
    builder.Services.AddControllers(config =>
    {
        config.RespectBrowserAcceptHeader = true;
        config.ReturnHttpNotAcceptable = true;

    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.ConfigureExceptionHandler();
    app.UseHttpsRedirection();
    app.UseCors("CorsPolicy");
    app.UseAuthorization();
    app.MapControllers();

    logger.Info("Application started successfully");
    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit
    NLog.LogManager.Shutdown();
}
