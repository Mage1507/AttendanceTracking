using AttendanceTracking.Data;
using Microsoft.EntityFrameworkCore;
using AttendanceTracking.Services;
using Serilog;
using FluentValidation.AspNetCore;
using System.Reflection;
using Azure.Identity;
using Microsoft.ApplicationInsights.Extensibility;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Configuration.AddAzureKeyVault(
        new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"),
        new DefaultAzureCredential());

        var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().WriteTo.ApplicationInsights(new TelemetryConfiguration { InstrumentationKey = builder.Configuration.GetValue<string>("InstrumentationKey") }, TelemetryConverter.Traces).CreateLogger();
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);



        builder.Services.AddControllers().AddFluentValidation(c => c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<DbInitializer>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("mssql")));
        builder.Services.AddScoped<DepartmentService>();
        builder.Services.AddScoped<ManagerService>();
        builder.Services.AddScoped<EmployeeService>();
        builder.Services.AddScoped<AttendanceService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseSwagger();
        app.UseSwaggerUI();


        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}