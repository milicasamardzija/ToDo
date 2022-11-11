using Azure.Storage;
using Microsoft.Extensions.Options;
using NovaLite.ToDo.Configuration;
using NovaLite.ToDo.Model;

namespace NovaLite.ToDo.Services
{
    public class AttachmentSasUriGenerator
    {
        private readonly BlobStorageConnectionString connectionString;

        public AttachmentSasUriGenerator(IOptions<BlobStorageConfiguration> options)
        {
            connectionString = BlobStorageConnectionString.FromString(options.Value.ConnectionString);
        }

        public string Generate(Attachment attachment, Azure.Storage.Sas.BlobSasPermissions permission)
        {
            var builder = new UriBuilder();
            builder.Scheme = "https";
            builder.Host = "";
            builder.Path = $"{connectionString.AccountName}.{connectionString.EndpointSuffix}/attachments/{attachment.Id}";
            builder.Query = GenerateSasQuery(attachment.Id.ToString(), permission).ToString();
            return builder.Uri.ToString();
        }

        private Azure.Storage.Sas.BlobSasQueryParameters GenerateSasQuery(string blobName, Azure.Storage.Sas.BlobSasPermissions permission)
        {
            Azure.Storage.Sas.BlobSasBuilder blobSasBuilder = new Azure.Storage.Sas.BlobSasBuilder()
            {
                BlobContainerName = "attachments",
                BlobName = blobName,
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(1),
            };
            blobSasBuilder.SetPermissions(permission);
            var crediential = new StorageSharedKeyCredential(connectionString.AccountName, connectionString.AccountKey);
            return blobSasBuilder.ToSasQueryParameters(crediential);
        }
    }
}
