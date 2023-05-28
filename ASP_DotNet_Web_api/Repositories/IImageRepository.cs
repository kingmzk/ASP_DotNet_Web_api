using ASP_DotNet_Web_api.Models.Domain;

namespace ASP_DotNet_Web_api.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
