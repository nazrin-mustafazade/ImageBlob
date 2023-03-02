using ImageUploader.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ImageUploader.Data
{
    public class FileDetailRepository:IFileDetailRepository
    {
        private DataContext _context;

        public FileDetailRepository(DataContext context)
        {
            _context = context;
        }

        public List<FileDetail> GetFileDetails()
        {
            return _context.FileDetail.ToList();
        }
    }
}
