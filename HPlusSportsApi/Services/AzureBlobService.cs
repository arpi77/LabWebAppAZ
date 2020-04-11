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
            return null;
        }
    }
}
