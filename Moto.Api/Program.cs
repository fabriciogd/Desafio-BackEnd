using Microsoft.AspNetCore.Mvc;
using Moto.Persistence;
using Moto.Application;
using System.Text.Json;
using Moto.Infraestructure;
using Serilog;
using Moto.BackgroundTasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using Moto.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(builder.Configuration));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddHttpContextAccessor()
    .AddApiVersioning(versioningOptions =>
    {
        versioningOptions.DefaultApiVersion = ApiVersion.Default;
        versioningOptions.ReportApiVersions = true;
        versioningOptions.AssumeDefaultVersionWhenUnspecified = true;
    })
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(x => x.EnableAnnotations())
    .AddControllers(cfg =>
    {
        var noContentFormatter = cfg.OutputFormatters.OfType<HttpNoContentOutputFormatter>().FirstOrDefault();
        if (noContentFormatter != null)
        {
            noContentFormatter.TreatNullValueAsNoContent = false;
        }
    })
    .AddJsonOptions(
        options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        }); ;

builder.Services
    .AddServices(builder.Configuration)
    .AddPersistence(builder.Configuration)
    .AddRepositories()
    .AddCommandHandlers()
    .AddBackgroundTasks(builder.Configuration);

builder.Host.UseDefaultServiceProvider((context, serviceProviderOptions) =>
{
    serviceProviderOptions.ValidateScopes = context.HostingEnvironment.IsDevelopment();
    serviceProviderOptions.ValidateOnBuild = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MotoDbContext>();
    db.Database.Migrate();
}

app.Run();