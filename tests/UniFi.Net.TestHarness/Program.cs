using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UniFi.Net.TestHarness;
using UniFi.Net.TestHarness.Network;
using UniFi.Net.TestHarness.SiteManager;

var builder = Host.CreateApplicationBuilder();

builder.Configuration.AddUserSecrets<Program>(optional: true, reloadOnChange: true);

Uri? keyVault = builder.Configuration.GetValue<Uri?>("Azure:KeyVault:Uri");

if (keyVault is not null)
{
    builder.Configuration.AddAzureKeyVault(keyVault, new DefaultAzureCredential());
}

builder.Services.AddUniFiNetworkClient(config => builder.Configuration.GetSection("UniFi:Network").Bind(config));
builder.Services.AddUniFiSiteManagerClient(config => builder.Configuration.GetSection("UniFi:SiteManager").Bind(config));

builder.Services.AddSingleton<NetworkClient>();
builder.Services.AddSingleton<SiteManagerClient>();

builder.Services.AddHostedService<App>();

var app = builder.Build();

app.Run();

Console.ReadLine();