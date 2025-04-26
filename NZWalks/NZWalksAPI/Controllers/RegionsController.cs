using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Controllers
{
    // http://localhost:1234/api/regions
    [Route("api/[controller]")] 
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbcontext;

        public RegionsController(NZWalksDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        // GET ALL REGIONS
        // GET: http://localhost:portno/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            // get data from database and this is your domain models.
            var regionsDomain = dbcontext.Regions.ToList();
            
            //Map domain models to dtos
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

            //return DTOs
            return Ok(regionsDto);
        }

        // GET SINGLE REGION (Get region by id)
        // GET: http://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]// id should match the below id
        public IActionResult GetById([FromRoute]Guid id)
        {
            //Method 1: of getting a single resource by id uisng find(it will only take the primary key so cant use for any other method like code name or image)
            //var region = dbcontext.Regions.Find(id);

            //Method 2: using first or default using link u method
            //GEt Region Domain Model from the  Database
            var regionDomain = dbcontext.Regions.FirstOrDefault(x  => x.Id == id);
            
            if (regionDomain == null)
            {
                return NotFound();
            }
            //Map or convert the region domain model to region dto

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            //Return dto back to client
            return Ok(regionDto);
        }


        [HttpPost]
        //POST to Create New Region 
        //POST : http//localhost:portnumber/api/regions
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //map or convert the DTO to Domain model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl= addRegionRequestDto.RegionImageUrl
            };


            //Use Domain Model to Create Region
            dbcontext.Regions.Add(regionDomainModel);
            dbcontext.SaveChanges();
            //Map domain model back to DTO
            var regionDto = new RegionDto 
            { 
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl

            };

            return CreatedAtAction(nameof(GetById), new {id = regionDomainModel.Id}, regionDomainModel);
        }



        //Update Region
        //PUT: http//localhost:portnumber/api/regions/{id}

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Check if Region exists
            var regionDomainModel = dbcontext.Regions.FirstOrDefault(x => x.Id == id);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            //Map DTO to Domain Model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            dbcontext.SaveChanges();
            //COnvert Domain Model to Dto

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);

        }

        //Delete Region
        //DELETE: http//localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delet([FromRoute] Guid id)
        {
            var regionDomainModel = dbcontext.Regions.FirstOrDefault(x => x.Id == id);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            //delete Region
            dbcontext.Regions.Remove(regionDomainModel);
            dbcontext.SaveChanges();

            //return deleted Region back
            //map Domain Model to DTO

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDomainModel);
        }
    }
}
