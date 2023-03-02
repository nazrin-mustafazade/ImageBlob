using ImageUploader.Models;
using System.Collections.Generic;

namespace ImageUploader.Data
{
    public interface IFileDetailRepository
    {
        List<FileDetail> GetFileDetails();

    }
}
