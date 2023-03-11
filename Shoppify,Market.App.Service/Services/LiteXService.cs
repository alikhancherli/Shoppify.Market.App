using LiteX.Storage.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Shoppify.Market.App.Domain.Markers;
using Shoppify.Market.App.Service.Options;
using Shoppify.Market.App.Service.Services.Contracts;

namespace Shoppify.Market.App.Service.Services
{
    public class LiteXService : ILiteXService, IScopedDependency
    {
        private readonly ILiteXBlobServiceAsync _liteXBlob;
        private LiteXOptons _liteXOptons;

        public LiteXService(
            ILiteXStorageProviderFactory factory,
            IOptionsMonitor<LiteXOptons> liteXOptionsMonitor)
        {
            _liteXBlob = factory.GetStorageProvider("MyProvider");

            _liteXOptons = liteXOptionsMonitor.CurrentValue;
            liteXOptionsMonitor.OnChange(_ => _liteXOptons = _);
        }

        public async Task<bool> CreateContainerAsync(string containerName)
        {
            return await _liteXBlob.CreateContainerAsync(Path.Combine(_liteXOptons.Directory, containerName));
        }

        public async Task<IList<BlobDescriptor>> GetBlobsAsync()
        {
            return await _liteXBlob.GetBlobsAsync(_liteXOptons.Directory);
        }

        public async Task<IList<BlobDescriptor>> GetBlobsAsync(string containerName)
        {
            return await _liteXBlob.GetBlobsAsync(containerName);
        }

        public async Task<string> GetBlobUrlAsync(string blobName)
        {
            return await _liteXBlob.GetBlobUrlAsync(blobName);
        }

        public async Task<string> GetBlobUrlAsync(string containerName, string blobName)
        {
            return await _liteXBlob.GetBlobUrlAsync(blobName, containerName);
        }

        public async Task<IList<string>> GetContainersAsync()
        {
            return await _liteXBlob.GetAllContainersAsync();
        }

        public async Task<bool> UploadBlobAsync(IFormFile formFile, bool isPublic = true)
        {
            var fileName = formFile.FileName;
            Stream fileStream = formFile.OpenReadStream();
            var contentType = formFile.ContentType;
            var blobProperties = new BlobProperties()
            {
                ContentType = contentType,
                Security = isPublic ? BlobSecurity.Public : BlobSecurity.Private
            };
            return await _liteXBlob.UploadBlobAsync(_liteXOptons.Directory, fileName, fileStream, blobProperties);
        }

        public async Task<bool> UploadBlobAsync(string containerName, IFormFile formFile, bool isPublic = true)
        {
            var fileName = formFile.FileName;
            Stream fileStream = formFile.OpenReadStream();
            var contentType = formFile.ContentType;
            var blobProperties = new BlobProperties()
            {
                ContentType = contentType,
                Security = isPublic ? BlobSecurity.Public : BlobSecurity.Private
            };
            return await _liteXBlob.UploadBlobAsync(Path.Combine(_liteXOptons.Directory, containerName), fileName, fileStream, blobProperties);
        }
    }
}
