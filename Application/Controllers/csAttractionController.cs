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
using DbModels;

namespace AppWebbApi.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class csAttractionController: Controller
    {
        private ILogger<csAttractionController> _logger = null;
        private IAttractionService _service = null;
        
        //endpoint to read all attractions
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

        //endpoint to read one attraction
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

        //endpoint to read one attraction in DTO format
        [HttpGet()]
        [ActionName("ReadAttractionDto")]
        [ProducesResponseType(200, Type = typeof(csAttractionCUdto))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadAttractionDto(string AttractionId = null)
        {
            try
            {
                _logger.LogInformation("Endpoint ReadAttractionDto executed");
                var _id = Guid.Parse(AttractionId);
                
                var item = _service.ReadAttraction(_id);
                if (item == null)
                {
                    return BadRequest($"Item with id {AttractionId} does not exist");
                }

                var dto = new csAttractionCUdto(item);
                return Ok(dto);          
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint ReadAttractionDto error");
                return BadRequest(ex.Message);
            }
        }

        //endpoint to read all attractions in a specified city
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

        //endpoint to read all attractions that does not have any comments
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

        //endpoint to add a new attraction
        [HttpPost()]
        [ActionName("AddAttraction")]
        [ProducesResponseType(200, Type = typeof(IAttraction))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> AddAttraction([FromBody] csAttractionCUdto item)
        {
            try
            {
                _logger.LogInformation("Endpoint AddAttraction executed");

                var _item = _service.AddAttraction(item);
                _logger.LogInformation($"item {_item.AttractionId} created");

                return Ok(_item);      
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint AddAttraction error");
                return BadRequest(ex.Message);
            }
        }

        //endpoint to update an existing attraction
        [HttpPut("{id}")]
        [ActionName("UpdateAttraction")]
        [ProducesResponseType(200, Type = typeof(csAttractionCUdto))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> UpdateAttraction(string id, [FromBody] csAttractionCUdto item)
        {
            try
            {
                _logger.LogInformation("Endpoint UpdateAttraction executed");

                Guid _id = Guid.Parse(id);
                if (item.AttractionId != _id)
                    throw new Exception("Id mismatch");

                var _item = _service.UpdateAttraction(item);
                _logger.LogInformation($"item {_id} updated");
                
                return Ok(_item);
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint UpdateAttraction error");
                return BadRequest(ex.Message);
            }
            
        }

        //endpoint to remove an existing attraction
        [HttpDelete()]
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

        //constructor that injects logger and service
        public csAttractionController(IAttractionService service, ILogger<csAttractionController> logger)
        {
            _service = service;
            _logger = logger;
        }

    }
}



