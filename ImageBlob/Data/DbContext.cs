using ImageUploader.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageUploader.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<FileDetail> FileDetail { get; set; }
    }
}
