using ASP_DotNet_Web_api.CustomActionFilters;
using ASP_DotNet_Web_api.Models.domain;
using ASP_DotNet_Web_api.Models.DTO;
using ASP_DotNet_Web_api.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ASP_DotNet_Web_api.Controllers
{
    // api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        // CREATE Walk
        // POST: /api/walks
        [HttpPost]
        [ValidateModel]  //custom validated
        public async Task<IActionResult> CreateAsync([FromBody] AddWalkRequestDto addWalkRequestDto)
        {

            // Map DTO to Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            await walkRepository.CreateAsync(walkDomainModel);

            // Map Domain model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }



        //GET
        //GET : /api/walks
        //GET : /api/walks?filterOn=Name&filterQuery=Track?sortBy=Name&isAcending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAcending,
                                                    [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAcending ?? true, pageNumber, pageSize);

            //map domain model to dto
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }


        //get walk by id
        //get : api/walks/walk/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            //Map domain model to dto
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }


        //Update by Id
        //put:api/walks/walk/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]  //custom validated
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {

            //map dto to domain model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomainModel));


        }



        //DELETE
        //Delete : /api/walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteAysnc(id);

            if (deletedWalkDomainModel == null)
            {
                return null;
            }

            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));
        }
    }
}
