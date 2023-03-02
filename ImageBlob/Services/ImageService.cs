using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace ImageUploader.Services
{
    public class ImageService
    {
        private readonly IWebHostEnvironment _webhost;

        public ImageService(IWebHostEnvironment webhost)
        {
            _webhost = webhost;
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            if (file.Length != 0)
            {
                if(!Directory.Exists(Path.Combine(_webhost.WebRootPath, "images")))
                {
                   Directory.CreateDirectory(Path.Combine(_webhost.WebRootPath, "images"));

                }
                var saveimg = Path.Combine(_webhost.WebRootPath, "images", file.FileName);
                //string imgText = Path.GetExtension(file.FileName);
                using (var img = new FileStream(saveimg, FileMode.Create))
                {
                    await file.CopyToAsync(img);
                }
                return saveimg;
            }
            return "Not Ok";
        }
    }
}
