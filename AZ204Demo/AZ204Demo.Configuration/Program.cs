using Azure.Identity;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration((context, builder) =>
    {
        builder.AddAzureKeyVault(
            new Uri($"https://{context.Configuration["KeyVaultName"]}.vault.azure.net/"),
            new DefaultAzureCredential());

        builder.AddAzureAppConfiguration(options =>
            options.Connect(
                new Uri(context.Configuration["AppConfig:Endpoint"]),
                new DefaultAzureCredential()));

    })
    .ConfigureServices((context, services) =>
    {
        services.AddAzureClients(builder =>
        {
            builder.AddSecretClient(new Uri($"https://{context.Configuration["KeyVaultName"]}.vault.azure.net/"));

            builder
                .AddBlobServiceClient(context.Configuration["BlobStorageConnectionString"])
                .WithName("BlobStorageWithConnectionString");

            builder
                .AddBlobServiceClient(new Uri(context.Configuration["BlobStorageUrl"]))
                .WithName("BlobStorageWithUrl");
        });
    })
    .Build();

host.Run();
