using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Seido.Utilities.SeedGenerator;

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
        [ActionName("Attractions")]
        [ProducesResponseType(200, Type = typeof(List<csAttraction>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ListAttractions(string count = "5")
        {
            try
            {
                _logger.LogInformation("Endpoint ListAttraction executed");
                int _count = int.Parse(count);
                var attractons = _service.ListAttractions(_count);
                
                return Ok(attractons);
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint ListAttraction error");
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



