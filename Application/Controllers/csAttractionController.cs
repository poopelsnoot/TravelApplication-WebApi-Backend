using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Seido.Utilities.SeedGenerator;
using Models.DTO;
using Models;
using Services;
using Configuration;

namespace AppWebbApi.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class csAttractionController: Controller
    {
        private ILogger<csAttractionController> _logger = null;
        private IAttractionService _service = null;
        
        [HttpGet()]
        [ActionName("ReadAttractions")]
        [ProducesResponseType(200, Type = typeof(List<IAttraction>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadAttractions(string count = "5", string category = "", string attractionName = "", string description = "", string country = "", string city = "")
        {
            try
            {
                _logger.LogInformation("Endpoint ReadAttractions executed");
                int _count = int.Parse(count);
                var attractons = _service.ReadAttractions(_count, category, attractionName, description, country, city);
                
                return Ok(attractons);
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint ReadAttractions error");
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet()]
        [ActionName("ReadAttraction")]
        [ProducesResponseType(200, Type = typeof(IAttraction))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadAttraction(string AttractionId)
        {
            try
            {
                _logger.LogInformation("Endpoint ReadAttraction executed");
                var id = Guid.Parse(AttractionId);
                var attracton = _service.ReadAttraction(id);
                
                return Ok(attracton);
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint ReadAttraction error");
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet()]
        [ActionName("ReadAttractionsByCity")]
        [ProducesResponseType(200, Type = typeof(List<IAttraction>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadAttractionsByCity(string city)
        {
            try
            {
                _logger.LogInformation("Endpoint ReadAttractionsByCity executed");
                var attractons = _service.ReadAttractionsByCity(city);
                
                return Ok(attractons);
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint ReadAttractionsByCity error");
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet()]
        [ActionName("ReadAttractionsWithoutComments")]
        [ProducesResponseType(200, Type = typeof(List<IAttraction>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadAttractionsWithoutComments()
        {
            try
            {
                _logger.LogInformation("Endpoint ReadAttractionsWithoutComments executed");
                var attractons = _service.ReadAttractionsWithoutComments();
                
                return Ok(attractons);
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint ReadAttractionsWithoutComments error");
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet()]
        [ActionName("RemoveAttraction")]
        [ProducesResponseType(200, Type = typeof(List<IAttraction>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> RemoveAttraction(string Id)
        {
            try
            {
                _logger.LogInformation("Endpoint RemoveAttraction executed");
                Guid _id = Guid.Parse(Id);
                adminInfoDbDto _info = _service.RemoveAttraction(_id);
                
                return Ok(_info);
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint RemoveAttraction error");
                return BadRequest(ex.Message);
            }
            
        }
    
        public csAttractionController(IAttractionService service, ILogger<csAttractionController> logger)
        {
            _service = service;
            _logger = logger;
        }

    }
}



