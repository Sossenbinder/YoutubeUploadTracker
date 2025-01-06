using Asp.Versioning;
using YoutubeUploadTracker.Api.Features.Common;
using YoutubeUploadTracker.Api.Features.Common.Auth;
using YoutubeUploadTracker.Api.Features.Youtube;
using YoutubeUploadTracker.Api.Infrastructure.BackgroundServices;
using YoutubeUploadTracker.Api.Infrastructure.Hangfire;
using YoutubeUploadTracker.Api.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("appsettings.local.json", true);
}

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

RegisterCustomServices();

builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new(1);
    x.ReportApiVersions = true;
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
});

builder.Services.AddCors(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.AddDefaultPolicy(corsPolicyBuilder =>
        {
            corsPolicyBuilder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    }
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

RegisterAppExtensions();

app.UseHttpsRedirection();

app.UseCookiePolicy(new()
{
    MinimumSameSitePolicy = SameSiteMode.Lax
});

app.UseAuthentication();
app.UseAuthorization();

app.UseCors();

app.Run();
return;

void RegisterCustomServices()
{
    builder.Logging.AddCustomSerilog(builder.Configuration, builder.Environment);

    builder.Services.AddHostedService<EfMigrationService>();

    builder.Services
        .RegisterCustomAuth(builder.Configuration)
        .RegisterCommonFunctionality(builder.Configuration)
        .RegisterYoutube(builder.Configuration);

    builder.Services.AddCloudHangFire(builder.Configuration);
}

void RegisterAppExtensions()
{
    app.AddHangfireDashboard();
    SetupEndpoints();
}

void SetupEndpoints()
{
    app.MapControllers();

    app.MapGroup("/api")
        .MapYoutubeEndpoints()
        .MapAuthenticationEndpoints(app.Services);
}