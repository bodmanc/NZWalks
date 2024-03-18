using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories.Interfaces;
using Serilog.Data;
using System.Collections.Generic;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    // https://localhost:portnumber/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext context;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext context, IRegionRepository regionRepository,
            IMapper mapper, ILogger<RegionsController> logger)
        {
            this.context = context;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            // Get data from database - Domain models
            var regionDomain = await regionRepository.GetAllAsync();

            // Map domain model to dto
            // Return DTOs to client
            return Ok(mapper.Map<List<RegionDto>>(regionDomain));

        }

        // GET SINGLE REGION (Get region by Id)
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {

            // Get region domain model from database
           var regionDomain = await regionRepository.GetByIdAsync(id);
          
           if (regionDomain == null)
           {
               return NotFound();
           }
            // Map region domain model to dto
            // Return dto back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        // POST TO CREATE NEW REGION
        // POST: https://localhost:portnumber/api/regions
        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            
                // Map dto to domain model
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

                // Use domain model to create region
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                // Map domain model back to dto
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // UPDATE REGION
        // PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
                // Map dto to domain
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

                // Check if region exists
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                // Map domain model to dto
                // Return dto to client
                return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }

        // DELETE REGION
        // DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
         //[Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
           var regionDomainModel = await regionRepository.DeleteAsync(id);

           if (regionDomainModel == null)
           {
               return NotFound();
           }

           // Return delete region back
           // Map domain model to dto
           return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }

    }
}
