using System;
using System.Net;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AZ204Demo.Configuration
{
    public class Functions
    {
        private readonly SecretClient _secretClient;
        private readonly BlobServiceClient _blobClientWithConnectionString;
        private readonly BlobServiceClient _blobClientWithUrl;

        public Functions(
            SecretClient secretClient,
            IAzureClientFactory<BlobServiceClient> clientFactory)
        {
            _secretClient = secretClient;
            _blobClientWithConnectionString = clientFactory.CreateClient("BlobStorageWithConnectionString");
            _blobClientWithUrl = clientFactory.CreateClient("BlobStorageWithUrl");
        }

        [Function("GetSecret")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            var secret = await _secretClient.GetSecretAsync("");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString(secret.Value.Value);

            return response;
        }

        [Function("GetBlobFromConnectionString")]
        public async Task<HttpResponseData> GetBlobFromConnectionString([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            BlobContainerClient containerClient = await _blobClientWithUrl.CreateBlobContainerAsync("");
            BlobClient blobClient = containerClient.GetBlobClient("");

            BlobDownloadResult downloadResult = await blobClient.DownloadContentAsync();
            string blobContents = downloadResult.Content.ToString();

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString(blobContents);

            return response;
        }

        [Function("GetBlobFromUrl")]
        public async Task<HttpResponseData> GetBlobFromUrl([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            BlobContainerClient containerClient = await _blobClientWithConnectionString.CreateBlobContainerAsync("");
            BlobClient blobClient = containerClient.GetBlobClient("");

            BlobDownloadResult downloadResult = await blobClient.DownloadContentAsync();
            string blobContents = downloadResult.Content.ToString();

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString(blobContents);

            return response;
        }
    }
}
