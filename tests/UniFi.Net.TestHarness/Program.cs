using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UniFi.Net.TestHarness;

var builder = Host.CreateApplicationBuilder();

builder.Configuration.AddUserSecrets<Program>(optional: true, reloadOnChange: true);

builder.Services.AddNetworkClient(config => builder.Configuration.GetSection("UniFi").Bind(config));

builder.Services.AddHostedService<App>();

var app = builder.Build();

app.Run();

Console.ReadLine();