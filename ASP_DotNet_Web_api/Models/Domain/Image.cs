using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_DotNet_Web_api.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        [NotMapped]             // it sayd class or property excluded
        public IFormFile File { get; set; }

        public string FileName { get; set; }

        public string? FileDescription { get; set; }

        public string FileExtention { get; set; }

        public long FileSizeInBytes { get; set; }

        public string FilePath { get; set; }
    }
}
