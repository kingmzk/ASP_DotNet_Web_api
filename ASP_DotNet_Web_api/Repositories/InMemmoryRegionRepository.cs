using ASP_DotNet_Web_api.Models.domain;

namespace ASP_DotNet_Web_api.Repositories
{
    public class InMemmoryRegionRepository //: IRegionRepository
    {
        public async Task<List<Region>> GetAllAsync()
        {
            return new List<Region>
            {
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Code = "MZK",
                    Name = "Mohammed Zakria Khan",
                    RegionImageUrl ="https://tse1.mm.bing.net/th?&id=OVP.HoLjiQc4Vlx6TJVvvbNtNgIID1&w=348&h=165&c=7&pid=2.1&rs=1"
                }
            };
        }
    }
}
