using Asp.Versioning;
using Asp.Versioning.Conventions;
using YoutubeUploadTracker.Api.Features.Common;
using YoutubeUploadTracker.Api.Features.Youtube;
using YoutubeUploadTracker.Api.Features.Youtube.Api;
using YoutubeUploadTracker.Api.Infrastructure.BackgroundServices;
using YoutubeUploadTracker.Api.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("appsettings.local.json", true);
}

builder.Logging.AddCustomSerilog(builder.Configuration, builder.Environment);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<EfMigrationService>();

builder.Services
    .RegisterCommonFunctionality(builder.Configuration)
    .RegisterYoutube(builder.Configuration);

builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1);
    x.ReportApiVersions = true;
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
});

var app = builder.Build();

var versionSet = app.NewApiVersionSet()
    .HasApiVersion(1.0)
    .HasApiVersion(2.0)
    .ReportApiVersions()
    .Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UsePathBase(new PathString("/api"));

app.MapControllers();
app.MapYoutubeChannelEndpoints(versionSet);

app.Run();
