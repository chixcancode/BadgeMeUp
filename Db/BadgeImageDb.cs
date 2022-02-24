using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

namespace BadgeMeUp.Db
{
    public class BadgeImageDb
    {
        private readonly IConfiguration _configuration;

        private const string ContainerName = "badge-images";
        private const string ImageNameFormat = "badge-{0}.jpg";

        public BadgeImageDb(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private BlobContainerClient GetContainerClient()
        {
            var connectionString = _configuration.GetConnectionString("badgeImageStorage");
            var container = new BlobContainerClient(connectionString, ContainerName);
            container.CreateIfNotExists();

            return container;
        }
        private static string GetBadgeFileName(int badgeId)
        {
            return string.Format(ImageNameFormat, badgeId);
        }

        public async Task SaveBadgeImage(int badgeId, Stream imageStream, string contentType)
        {
            var blobHttpHeaders = new BlobHttpHeaders();
            blobHttpHeaders.ContentType = contentType;

            var container = GetContainerClient();
            var blobClient = container.GetBlobClient(GetBadgeFileName(badgeId));
            imageStream.Position = 0;
            await blobClient.UploadAsync(imageStream, blobHttpHeaders);
        }

        public string GetBadgeImageUrl(int badgeId)
        {
            var start = DateTime.UtcNow;
            var container = GetContainerClient();
            var blobClient = container.GetBlobClient(GetBadgeFileName(badgeId));
            var sas = blobClient.GenerateSasUri(BlobSasPermissions.Read, new DateTimeOffset(DateTime.UtcNow.AddYears(100))).AbsoluteUri;
            var end = DateTime.UtcNow;

            Console.WriteLine((end - start).ToString());

#if DEBUG == true
            // This is to handle the docker-compose case where the SAS URI is local to the docker network, not the actual host
            sas = sas.Replace("badgemeup-storage", "localhost");
#endif

            return sas;
        }
    }
}
