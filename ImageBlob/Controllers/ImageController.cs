using ImageUploader.Data;
using ImageUploader.Models;
using ImageUploader.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageUploader.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {

        private readonly IWebHostEnvironment _webhost;
        private readonly IFileDetailRepository _fileDetailRepository;
        public ImageController(IWebHostEnvironment webhost, IFileDetailRepository fileDetailRepository)
        {
            _webhost = webhost;
            _fileDetailRepository = fileDetailRepository;
        }
        public IActionResult Get()
        {
            List<FileDetail> fileDetails = new List<FileDetail>();
            fileDetails = _fileDetailRepository.GetFileDetails();
            return Ok(fileDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {

            var image = await new ImageService(_webhost).SaveFile(file);
            if (image == "Not Ok")
            {
                return BadRequest();

            }
            else
            {
                string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=nazrin;AccountKey=m5W+q8pwHOG0bAKap3J0e3yjbXnTfWvEJ2SR6l/h5Gl4NVdhoc1A4Diyp0O7TqsHhbOk79HfssLr+ASto9qxmQ==;EndpointSuffix=core.windows.net";
                CloudStorageAccount storageacc = CloudStorageAccount.Parse(storageConnectionString);

                CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("images");

                CloudBlockBlob blockBlob = container.GetBlockBlobReference(file.FileName);
                blockBlob.Properties.ContentType = "image/jpg";
                using (var filestream = file.OpenReadStream())
                {
                    await blockBlob.UploadFromStreamAsync(filestream);
                }
                return Ok(blockBlob);
            }
        }
    }
}
