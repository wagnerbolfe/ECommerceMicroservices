using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot().AddCacheManager(settings => settings.WithDictionaryHandle());

builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);

builder.Logging.AddConfiguration(builder.Configuration.GetSection(("Logging")));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

app.MapGet("/", () => "Ocelot API Gateway");

await app.UseOcelot();

app.Run();
