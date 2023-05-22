using ASP_DotNet_Web_api.Models.domain;

namespace ASP_DotNet_Web_api.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();  //GetAllAsync return List it is async so using Task

        Task<Region?> GetByIdAsync(Guid id);

        Task<Region> CreateAsync(Region region);

        Task<Region?> UpdateAsync(Guid id, Region region);

        Task<Region?> DeleteAsync(Guid id);

        Task<Region?> PatchAsync(Guid id, Region region);
    }
}
