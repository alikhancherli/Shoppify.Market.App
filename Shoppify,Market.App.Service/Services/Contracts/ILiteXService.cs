using LiteX.Storage.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppify.Market.App.Service.Services.Contracts
{
    public interface ILiteXService
    {
        Task<IList<BlobDescriptor>> GetBlobsAsync();
        Task<IList<BlobDescriptor>> GetBlobsAsync(string containerName);
        Task<string> GetBlobUrlAsync(string blobName);
        Task<string> GetBlobUrlAsync(string containerName,string blobName);
        Task<bool> UploadBlobAsync(IFormFile formFile,bool isPublic = true);
        Task<bool> UploadBlobAsync(string containerName,IFormFile formFile, bool isPublic = true);
        Task<IList<string>> GetContainersAsync();
        Task<bool> CreateContainerAsync(string containerName);
    }
}
