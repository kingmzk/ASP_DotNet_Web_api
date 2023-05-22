using ASP_DotNet_Web_api.Data;
using ASP_DotNet_Web_api.Models.domain;
using Microsoft.EntityFrameworkCore;

namespace ASP_DotNet_Web_api.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly MZWalksDbContext dbContext;

        public SQLRegionRepository(MZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            return region;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                // Handle the case when the existing region was not found.
                // You can choose to return null or throw an exception.
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            return existingRegion;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();

            return existingRegion;
        }

        public async Task<Region?> PatchAsync(Guid id, Region region)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                // Handle the case when the existing region was not found.
                // You can choose to return null or throw an exception.
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
