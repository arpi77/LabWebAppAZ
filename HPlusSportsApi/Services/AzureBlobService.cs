using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HPlusSportsApi.Services
{
    /// <summary>
    /// Manages saving images to blob storage
    /// </summary>
    public class AzureBlobService : IBlobService
    {
        IConfiguration config;
        string containerName;

        public AzureBlobService(IConfiguration configuration)
        {
            config = configuration;
            containerName = config[Constants.KEY_BLOB];
        }

        public async Task<Models.BlobReference> GetBlobForWriteAsync(string blobName)
        {
            CloudStorageAccount acct = CloudStorageAccount.Parse(
                config[Constants.KEY_STORAGE_CNN]);

            var blobClient = acct.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            var blob = container.GetBlockBlobReference(blobName);

            //set properties
            blob.Properties.ContentType = "image/jpeg";
            blob.Properties.CacheControl = "public";

            var blobStream = await blob.OpenWriteAsync();
            return new Models.BlobReference
            {
                BlobStream = blobStream,
                BlobUri = blob.Uri.ToString()
            };
        }
    }
}
