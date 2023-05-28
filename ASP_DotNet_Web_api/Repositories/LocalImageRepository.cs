using ASP_DotNet_Web_api.Data;
using ASP_DotNet_Web_api.Models.Domain;

namespace ASP_DotNet_Web_api.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly MZWalksDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, MZWalksDbContext dbContext) // provides information about web host environment
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName} {image.FileExtention}");


            //upload File to Local Path
            using var Stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(Stream);

            //http//localhost:1234/images/images.jpg   -> we are using IHttpContextAccessor

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtention}";

            image.FilePath = urlFilePath;

            //add image to the images table 
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
    }
}
