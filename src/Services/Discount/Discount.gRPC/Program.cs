using Discount.gRPC.Extensions;
using Discount.gRPC.Repositories;
using Discount.gRPC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(8003, opt => opt.Protocols = HttpProtocols.Http2);
    options.Listen(IPAddress.Any, 80, opt => opt.Protocols = HttpProtocols.Http2);
    options.Listen(IPAddress.Any, 5003, opt => opt.Protocols = HttpProtocols.Http2);
});
 
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddGrpc();

var app = builder.Build();

app.MigrateDatabase<Program>();

app.MapGrpcService<DiscountService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, " +
                      "visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
