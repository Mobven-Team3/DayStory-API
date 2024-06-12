using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.EntityFrameworkCore;
using DayStory.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using DayStory.WebAPI.Middlewares;
using Serilog.Exceptions;
using FluentValidation.AspNetCore;
using DayStory.Application.Validators;
using FluentValidation;
using DayStory.Application.Services;
using MovieAPI.WebAPI.AutoFac;
using DayStory.Application.Options;
using DayStory.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);
ConfigureLogging();
var config = builder.Configuration;

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        corsBuilder =>
        {
            corsBuilder
                       .AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
        });
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(x => x.RegisterModule(new AutoFacModule()));

builder.Services.AddControllers().AddFluentValidation(fv =>
{
    fv.DisableDataAnnotationsValidation = true;
    fv.LocalizationEnabled = false;
});

builder.Services.AddValidatorsFromAssemblyContaining<UserRegisterContractValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DayStoryAPIDbContext>(builder =>
{
    builder.UseNpgsql(config.GetConnectionString("PostgreSqlConnection"));
});

builder.Services.AddProblemDetails();

JwtConfig jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();
builder.Services.Configure<JwtConfig>(config.GetSection("JwtConfig"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig.Issuer,
        ValidAudience = jwtConfig.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Secret)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.Configure<OpenAIOptions>(config.GetSection("OpenAI"));
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IOpenAIService, OpenAIService>();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseStaticFiles();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

void ConfigureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment}.json", optional: true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .WriteTo.Console()
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
};
