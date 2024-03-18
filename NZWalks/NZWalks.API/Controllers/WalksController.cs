using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories.Interface;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Controllers
{
    // /api/walks
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

        // CREATE WALK
        // POST: /api/walks

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
                // Map Dto to domain model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

                await walkRepository.CreateAsync(walkDomainModel);

                // Map domain model to dto
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        // GET WALKS
        // GET: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&IsAscending=true&pageNumber=1&pageSize=10

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true,
                pageNumber, pageSize);

            // Create an exception
            throw new Exception("This is a new exception");

            // map domain to dto
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        // GET WALK BY ID
        // GET: /api/walks/{id}

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id) 
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();  
            }

            // Map domain model to dto
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        // UPDATE WALK BY ID 
        // PUT: /api/walks/{id}

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateByIdAsync([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
                // Map dto to domain model
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

                if (walkDomainModel == null)
                {
                    return NotFound();
                }

                // Map domain to dto
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        // DELETE WALK BY ID
        // DELETE: /api/walks/{id}

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.DeleteAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            // Map domain model to dto
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }
    }
}
