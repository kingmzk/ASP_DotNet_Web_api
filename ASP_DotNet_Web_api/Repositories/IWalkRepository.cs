using ASP_DotNet_Web_api.Models.domain;

namespace ASP_DotNet_Web_api.Repositories
{

    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);

        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string?
            sortBy = null, bool isAcending = true);

        Task<Walk?> GetByIdAsync(Guid id);

        Task<Walk?> UpdateAsync(Guid id, Walk walk);

        Task<Walk?> DeleteAysnc(Guid id);
    }
}
