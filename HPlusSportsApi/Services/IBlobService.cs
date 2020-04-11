using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HPlusSportsApi.Services
{
    public interface IBlobService
    {
        Task<Models.BlobReference> GetBlobForWriteAsync(string blobName);
    }
}
