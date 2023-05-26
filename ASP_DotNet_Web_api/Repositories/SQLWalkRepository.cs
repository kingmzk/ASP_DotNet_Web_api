using ASP_DotNet_Web_api.Data;
using ASP_DotNet_Web_api.Models.domain;
using Microsoft.EntityFrameworkCore;

namespace ASP_DotNet_Web_api.Repositories
{

    public class SQLWalkRepository : IWalkRepository
    {
        private readonly MZWalksDbContext dbcontext;

        public SQLWalkRepository(MZWalksDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbcontext.Walks.AddAsync(walk);
            await dbcontext.SaveChangesAsync();
            return walk;
        }


        public async Task<List<Walk>> GetAllAsync()
        {
            //    return await dbcontext.Walks.ToListAsync();
            return await dbcontext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbcontext.Walks
                   .Include("Difficulty")
                   .Include("Region")
                   .FirstOrDefaultAsync(x => x.Id == id);


        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbcontext.Walks.FindAsync(id);

            if (existingWalk == null)
            {
                return null;
            }

            // Update the properties of the existing walk with the new values
            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await dbcontext.SaveChangesAsync();

            return existingWalk;
        }


        public async Task<Walk?> DeleteAysnc(Guid id)
        {
            var existingWalk = await dbcontext.Walks.FindAsync(id);

            if (existingWalk == null)
            {
                return null;
            }

            dbcontext.Walks.Remove(existingWalk);
            await dbcontext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string?
            sortBy = null, bool isAcending = true, int pageNumber = 1, int pageSize = 1000)
        {
            //   return await dbcontext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            var walks = dbcontext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {

                // Filtering
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                    /*
                    walks = walks.Where(delegate (Walk x)
                    {
                        return x.Name.Contains(filterQuery);
                    });
                    */
                }

            }
            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAcending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAcending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }

            }

            //Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();

            //     return await walks.ToListAsync();
        }
    }
}
