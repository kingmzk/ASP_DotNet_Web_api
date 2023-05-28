using ASP_DotNet_Web_api.CustomActionFilters;
using ASP_DotNet_Web_api.Data;
using ASP_DotNet_Web_api.Models.domain;
using ASP_DotNet_Web_api.Models.DTO;
using ASP_DotNet_Web_api.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ASP_DotNet_Web_api.Controllers
{
    // http://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]

    public class RegionsController : ControllerBase
    {
        private readonly MZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(MZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        // Get :  http://localhost:1234/api/regions
        [HttpGet]
        //  [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            //throw exception
            throw new Exception("Exception is Not Correctly handled");

            logger.LogInformation("Getall Region all Methoď is invoked");
            logger.LogWarning("This is a Warning log");
            logger.LogError("THis is a Error Log");
            /*
            try                  //for logging in try catch
            {
                throw new Exception("This is custom Exception");
            }
            catch (Exception ex) 
            {
                logger.LogError(ex ,ex.Message);
                throw;
            }
            */
            // Get regions from the repository
            var regionsDomain = await regionRepository.GetAllAsync();

            // Map domain models to DTOs
            var regionDto = mapper.Map<List<RegionDto>>(regionsDomain);

            logger.LogInformation($"Finished getall Region all request with data : {JsonSerializer.Serialize(regionsDomain)}"); //converting object into json
            // Return DTOs to the client
            return Ok(regionDto);
        }

        // Get :  http://localhost:1234/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get a region by ID from the repository
            var regionDomain = await regionRepository.GetByIdAsync(id);

            // Check if the region exists
            if (regionDomain == null)
            {
                return NotFound();
            }

            // Map the domain model to a DTO
            var regionDto = mapper.Map<RegionDto>(regionDomain);

            // Return the DTO to the client
            return Ok(regionDto);
        }



        // POST :  http://localhost:1234/api/regions
        [HttpPost]
        [ValidateModel]  //custom validated
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            // Map the DTO to a domain model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            // Create a new region in the repository
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // Map the created domain model back to a DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            // Return the created DTO with the appropriate status code
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);



        }

        // PUT :  http://localhost:1234/api/regions/{id}
        [HttpPut]
        [ValidateModel]  //custom validated
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            // Map the DTO to a domain model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            // Check if the region exists in the repository and update it
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            // If the region was not found, return NotFound
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map the updated domain model to a DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            // Return the updated DTO to the client
            return Ok(regionDto);


        }

        // DELETE :  http://localhost:1234/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Delete a region by ID from the repository
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            // If the region was not found, return NotFound
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map the deleted domain model to a DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            // Return the deleted DTO to the client
            return Ok(regionDto);
        }

        // PATCH: http://localhost:1234/api/regions/{id}
        [HttpPatch]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Patch([FromRoute] Guid id, [FromBody] PatchRegionRequestDto patchRegionRequestDto)
        {
            // Check if the patch request body is null
            if (patchRegionRequestDto == null)
            {
                return BadRequest("Patch data is missing or invalid.");
            }

            // Map the DTO to a domain model
            var regionDomainModel = mapper.Map<Region>(patchRegionRequestDto);

            // Check if the region exists in the repository and update it partially
            var updatedRegion = await regionRepository.PatchAsync(id, regionDomainModel);

            // If the region was not found, return NotFound
            if (updatedRegion == null)
            {
                return NotFound();
            }

            // Apply the partial updates from the patch request to the updated region
            if (patchRegionRequestDto.Code != null)
            {
                updatedRegion.Code = patchRegionRequestDto.Code;
            }

            if (patchRegionRequestDto.Name != null)
            {
                updatedRegion.Name = patchRegionRequestDto.Name;
            }

            if (patchRegionRequestDto.RegionImageUrl != null)
            {
                updatedRegion.RegionImageUrl = patchRegionRequestDto.RegionImageUrl;
            }

            // No need to call dbContext.SaveChangesAsync() again since it's already done in the repository

            // Map the updated region domain model to a DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            // Return the updated region DTO to the client
            return Ok(regionDto);
        }
    }
}













/*
using ASP_DotNet_Web_api.Data;
using ASP_DotNet_Web_api.Models.domain;
using ASP_DotNet_Web_api.Models.DTO;
using ASP_DotNet_Web_api.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ASP_DotNet_Web_api.Controllers
{
    //http://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly MZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(MZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        // Get :  http://localhost:1234/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From DataBase  -> Domian Model
            //   var regionsDomain = dbContext.Regions.ToList();
            //  var regionsDomain = await dbContext.Regions.ToListAsync();
            var regionsDomain = await regionRepository.GetAllAsync();  // talkinğ with db through repository


            //Map Domain Model TO DTO (Data Transfer Object)
            /*
            var regionDto = new List<RegionDto>();

            foreach (var regionDomain in regionsDomain)
            {
                regionDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }
            */


/*
            //mapping domain model to DTO
            // var regionDto = mapper.Map<List<RegionDto>>(regionsDomain);
            //return DTOs
            // return Ok(regionDto);
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
        }

        //Get to Update a new Region
        // Get :  http://localhost:1234/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //        var region = dbContext.Regions.Find(id);  //it can be used only with ID (PRimary Key)
            //Get Data From DataBase  -> Domian Model
            // var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            // var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map Domain Model TO DTO (Data Transfer Object)
            /*
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            */
//return DTO back to client
//return Ok(regionDto);


/*
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        //Post to create a new Region
        // Post :  http://localhost:1234/api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Convert Dto  to Domain  Model
            /*
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            */

//Use  Domain Model to  create  Region
//    dbContext.Regions.Add(regionDomainModel);
// dbContext.SaveChanges();
//  await dbContext.Regions.AddAsync(regionDomainModel);  //added async 
//   await dbContext.SaveChangesAsync();                     //added async


/*
        //Map or Convert Dto  to Domain  Model
        var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

        regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);


        //map domain Model back to DTO
        /*
        var regionDto = new RegionDto
        {
            Id = regionDomainModel.Id,
            Name = regionDomainModel.Name,
            Code = regionDomainModel.Code,
            RegionImageUrl = regionDomainModel.RegionImageUrl
        };
        */


/*
//map domain Model back to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }


        //PUT to Update a new Region
        // PUT :  http://localhost:1234/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map DTO to domain model
            /*
            var regionDomainModel = new Region
            {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            };
            */
/*
            //Map DTO to domain model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            //check ire region exist
            //  var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //map to dto to domain model
            //   regionDomainModel.Code = updateRegionRequestDto.Code;
            //    regionDomainModel.Name = updateRegionRequestDto.Name;
            //   regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            //    await dbContext.SaveChangesAsync();

            /*
            //Convert Domain Model to  DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            */

/*
            //Convert Domain Model to  DTO
            var regionDto = mapper.Map<Region>(regionDomainModel);

            return Ok(regionDto);
        }

        // Delete Region 
        // Delete :  http://localhost:1234/api/regions/{id}
        [HttpDelete]
        [Route("{id}:Guid")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Deletet Region 
            //  dbContext.Regions.Remove(regionDomainModel);  //we dont have remove async
            //  await dbContext.SaveChangesAsync();

            // return deleted object back
            //map domain model to dto
            /*
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            */
/*
            //map domain model to dto
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }








        //Patch
        // PATCH to partially update a Region
        // PATCH: http://localhost:1234/api/regions/{id}
        [HttpPatch]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Patch([FromRoute] Guid id, [FromBody] PatchRegionRequestDto patchRegionRequestDto)
        {
            if (patchRegionRequestDto == null)
            {
                // The patch request body is null
                return BadRequest("Patch data is missing or invalid.");
            }

            // Create a new region domain model with the updated properties
            /*
            var regionDomainModel = new Region
            {
                Code = patchRegionRequestDto.Code,
                Name = patchRegionRequestDto.Name,
                RegionImageUrl = patchRegionRequestDto.RegionImageUrl
            };
            */
/*
            // Create a new region domain model with the updated properties
            var regionDomainModel = mapper.Map<Region>(patchRegionRequestDto);


            // Check if the region exists in the repository
            var updatedRegion = await regionRepository.PatchAsync(id, regionDomainModel);

            if (updatedRegion == null)
            {
                // The region with the specified ID was not found
                return NotFound();
            }

            // Apply the partial updates from the patch request to the updated region
            if (patchRegionRequestDto.Code != null)
            {
                updatedRegion.Code = patchRegionRequestDto.Code;
            }

            if (patchRegionRequestDto.Name != null)
            {
                updatedRegion.Name = patchRegionRequestDto.Name;
            }

            if (patchRegionRequestDto.RegionImageUrl != null)
            {
                updatedRegion.RegionImageUrl = patchRegionRequestDto.RegionImageUrl;
            }

            // No need to call dbContext.SaveChangesAsync() again since it's already done in the repository

            // Convert the updated region domain model to a DTO
            /*
            var regionDto = new RegionDto
            {
                Id = updatedRegion.Id,
                Code = updatedRegion.Code,
                Name = updatedRegion.Name,
                RegionImageUrl = updatedRegion.RegionImageUrl
            };
            */
/*
            // Convert the updated region domain model to a DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            // Return the updated region DTO
            return Ok(regionDto);
        }


    }
}




/*
  hardCoded

 var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Auckland Region",
                    Code = "AKL",
                    RegionImageUrl = "https://nomadsworld.com/wp-content/uploads/2017/08/auckland_night_bg_istock.jpg"
                },

                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Wellington Region",
                    Code = "WLG",
                    RegionImageUrl = "s"
                }

            };
            return Ok(regions);
*/
