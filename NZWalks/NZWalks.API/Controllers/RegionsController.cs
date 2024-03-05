using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Controllers
{
    // https://localhost:portnumber/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext context;

        public RegionsController(NZWalksDbContext context)
        {
            this.context = context;
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
           // Get data from database - Domain models
          var regionsDomain = context.Regions.ToList();

            // Map domain models to dto
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }

          // Return DTOs to client
          return Ok(regionsDto);
        }

        // GET SINGLE REGION (Get region by Id)
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute]Guid id)
        {

            // Get region domain model from database
           var regionDomain = context.Regions.FirstOrDefault(x => x.Id == id);
          
           if (regionDomain == null)
           {
               return NotFound();
           }
            // Map region domain model to dto

            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            // Return dto back to client
            return Ok(regionDto);
        }

        // POST TO CREATE NEW REGION
        // POST: https://localhost:portnumber/api/regions
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map dto to domain model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            // Use domain model to create region
            context.Regions.Add(regionDomainModel);
            context.SaveChanges();

            // Map domain model back to dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDto);
        }

        // UPDATE REGION
        // PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
          // Check if region exists
          var regionDomainModel = context.Regions.FirstOrDefault(x => x.Id == id);

          if (regionDomainModel == null)
          {
              return NotFound();
          }

            // Map dto to domain model
           regionDomainModel.Code = updateRegionRequestDto.Code;
           regionDomainModel.Name = updateRegionRequestDto.Name;
           regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;


           context.SaveChanges();


            // Map domain model to dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name, 
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            // Return dto to client
            return Ok(regionDto);
        }

        // DELETE REGION
        // DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            // Check if data exists
           var regionDomainModel = context.Regions.FirstOrDefault(x => x.Id == id);

           if (regionDomainModel == null)
           {
               return NotFound();
           }

            // Delete region
            context.Regions.Remove(regionDomainModel);
            context.SaveChanges();

            // Return delete region back
            // Map domain model to dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);

        }

    }
}
