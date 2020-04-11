using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HPlusSportsApi.Models
{
    /// <summary>
    /// Model class for a generic blob reference with a 
    /// writable stream and the URI
    /// </summary>
    public class BlobReference
    {
        public string BlobUri { get; set; }

        public Stream  BlobStream { get; set; }
    }
}
