using ASP_DotNet_Web_api.Models.domain;
using ASP_DotNet_Web_api.Models.DTO;
using AutoMapper;

namespace ASP_DotNet_Web_api.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            CreateMap<PatchRegionRequestDto, Region>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>();
        }
    }
}
