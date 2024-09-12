using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Mvc;
using ParamsService.Infrastructure.Data;
using SpeakerService.Api.model;

namespace SpeakerService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class imagesController : ControllerBase
    {
        private readonly MMC_Params _dbContext;
        private readonly BlobServiceClient _blobServiceClient;
        private const string ContainerName = "mmc";
        private readonly BlobContainerClient _containerClient;
        public string key = "O/jPRRQCyjEoHfQekkMsvb888JdEFrgT8yDZOdw22PODddDNw6HIYEiGBIn7dUX795/UetM44VBL+AStPIE06g==";
        public imagesController(MMC_Params ParamsDBContext, BlobServiceClient blobServiceClient)
        {
            _dbContext = ParamsDBContext;
            _blobServiceClient = blobServiceClient;
            _containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            _containerClient.CreateIfNotExists();
            
        }

        [HttpPost("/uploadsponsorimages")]
        public async Task<IActionResult> UploadSponsor(Images image)
        {
            string uniqueFileName = $"{image.EventId}_{image.File.FileName}";
            var blobClient = _containerClient.GetBlobClient(uniqueFileName);
            await blobClient.UploadAsync(image.File.OpenReadStream(), true);
            Ok("File uploaded successfully.");
            var Data = GetFileUrls(image.EventId);
            var sponsor = await _dbContext.Sponsors.FindAsync(image.EventId);
            if (sponsor == null)
            {
                throw new InvalidOperationException("event not found");
            }

            sponsor.Logo = Data;
            _dbContext.SaveChanges();
            return Ok("uploaded");
        }
        
        [HttpPost("/uploadpartnerimages")]
        public async Task<IActionResult> UploadPartner(Images image)
        {
            string uniqueFileName = $"{image.EventId}_{image.File.FileName}";
            var blobClient = _containerClient.GetBlobClient(uniqueFileName);
            await blobClient.UploadAsync(image.File.OpenReadStream(), true);
            Ok("File uploaded successfully.");
            var Data = GetFileUrls(image.EventId);
            var partner = await _dbContext.Partners.FindAsync(image.EventId);
            if (partner == null)
            {
                throw new InvalidOperationException("event not found");
            }

            partner.Logo = Data;
            _dbContext.SaveChanges();
            return Ok("uploaded");
        }

        [HttpGet("count")]
        public int CountFilesInContainer()
        {
            var blobs = _containerClient.GetBlobs();
            int fileCount = blobs.Count();

            return fileCount;
        }

        [HttpGet("names")]
        public IEnumerable<string> GetFileNamesInContainer()
        {
            var blobs = _containerClient.GetBlobs();

            var fileNames = new List<string>();
            foreach (var blobItem in blobs)
            {
                fileNames.Add(blobItem.Name);
            }

            return fileNames;
        }

        [HttpGet("url")]
        public string GetFileUrl(string fileName)
        {
            try
            {
                BlobClient blobClient = _containerClient.GetBlobClient(fileName);

                BlobSasBuilder sasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = _containerClient.Name,
                    BlobName = blobClient.Name,
                    Resource = "b",
                    StartsOn = DateTimeOffset.UtcNow,
                    ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
                };

                sasBuilder.SetPermissions(BlobSasPermissions.Read);
                StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(_containerClient.AccountName, key);

                string sasToken = sasBuilder.ToSasQueryParameters(sharedKeyCredential).ToString();
                Uri blobUrlWithSas = new Uri(blobClient.Uri, $"{fileName}?{sasToken}");

                return blobUrlWithSas.ToString();
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"Error: {ex.ErrorCode} - {ex.Message}");
                return null;
            }
        }

        [HttpGet("{fileId}")]
        public string GetFileUrls(Guid fileId)
        {
            try
            {
                List<string> urls = new List<string>();

                foreach (BlobItem blobItem in _containerClient.GetBlobs(prefix: fileId.ToString()))
                {
                    string fileName = blobItem.Name;
                    BlobClient blobClient = _containerClient.GetBlobClient(fileName);
                    BlobSasBuilder sasBuilder = new BlobSasBuilder()
                    {
                        BlobContainerName = _containerClient.Name,
                        BlobName = blobClient.Name,
                        Resource = "b",
                        StartsOn = DateTimeOffset.UtcNow,
                        ExpiresOn = DateTimeOffset.UtcNow.AddYears(10)
                    };
                    sasBuilder.SetPermissions(BlobSasPermissions.Read);
                    StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(_containerClient.AccountName, key);
                    string sasToken = sasBuilder.ToSasQueryParameters(sharedKeyCredential).ToString();
                    Uri blobUrlWithSas = new Uri(blobClient.Uri, $"{fileName}?{sasToken}");
                    urls.Add(blobUrlWithSas.ToString());
                }
                if (urls.Count() == 0)
                {
                    return "NaN";
                }
                else
                {
                    return urls[0];
                }
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"Error: {ex.ErrorCode} - {ex.Message}");
                return null;
            }
        }

        [HttpDelete("{fileId}")]
        public IActionResult DeleteBlobsById(string fileId)
        {
            try
            {
                foreach (BlobItem blobItem in _containerClient.GetBlobs(prefix: fileId))
                {
                    _containerClient.DeleteBlobIfExists(blobItem.Name);
                }

                return Ok($"Blobs with ID {fileId} deleted successfully.");
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"Error: {ex.ErrorCode} - {ex.Message}");
                return StatusCode(500, "An error occurred while deleting blobs.");
            }
        }
    }
}
