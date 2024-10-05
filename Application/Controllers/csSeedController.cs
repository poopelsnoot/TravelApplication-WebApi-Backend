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
    public class csSeedController: Controller
    {
        private ILogger<csSeedController> _logger = null;
        private ISeedService _service = null;
        
        [HttpGet()]
        [ActionName("SeedTestdata")]
        [ProducesResponseType(200, Type = typeof(adminInfoDbDto))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> SeedTestdata()
        {
            try
            {
                _logger.LogInformation("Endpoint SeedTestdata executed");
                adminInfoDbDto _info = _service.SeedTestdata();
                
                return Ok(_info);
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint SeedTestdata error");
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet()]
        [ActionName("RemoveAllTestdata")]
        [ProducesResponseType(200, Type = typeof(adminInfoDbDto))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> RemoveAllTestdata()
        {
            try
            {
                _logger.LogInformation("Endpoint RemoveAllTestdata executed");
                adminInfoDbDto _info = _service.RemoveAllTestdata(true);
                
                return Ok(_info);
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint RemoveAllTestdata error");
                return BadRequest(ex.Message);
            }
            
        }
    
        public csSeedController(ISeedService service, ILogger<csSeedController> logger)
        {
            _service = service;
            _logger = logger;
        }

    }
}