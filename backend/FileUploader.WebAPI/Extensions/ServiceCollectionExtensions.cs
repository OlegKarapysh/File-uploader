using FluentValidation;
using Microsoft.Extensions.Azure;
using FileUploader.WebAPI.Services.AzureFunctionTriggerService;
using FileUploader.WebAPI.Services.BlobStorageService;
using FileUploader.WebAPI.Validators;

namespace FileUploader.WebAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAzureBlobClient(this IServiceCollection services, IConfiguration config)
    {
        services.AddAzureClients(b =>
        {
            b.AddBlobServiceClient(config.GetConnectionString("AzureBlobStorageConnectionString"));
        });
    }

    public static void AddBlobStorageService(this IServiceCollection services)
    {
        services.AddScoped<IBlobStorageService, BlobStorageService>();
    }

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<IFormFile>, DocxFileValidator>();
        services.AddScoped<IValidator<string>, EmailValidator>();
    }

    public static void AddHttpClient(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpClient("AzureFunctionHttpClient", client =>
        {
            client.BaseAddress = new Uri(config["AzureFuncUrl"]);
        });
    }

    public static void AddAzureFunctionTriggerService(this IServiceCollection services)
    {
        services.AddScoped<IAzureFunctionTriggerService, AzureFunctionTriggerService>();
    }
}