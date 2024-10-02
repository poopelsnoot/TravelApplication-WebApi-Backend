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
    public class csUserController: Controller
    {
        private ILogger<csUserController> _logger = null;
        private IUserService _service = null;
        
        [HttpGet()]
        [ActionName("ReadUsers")]
        [ProducesResponseType(200, Type = typeof(List<IUser>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadUsers(string _count)
        {
            try
            {
                _logger.LogInformation("Endpoint ReadUsers executed");
                int count = int.Parse(_count);
                var users = _service.ReadUsers(count);
                
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint ReadUsers error");
                return BadRequest(ex.Message);
            }
            
        }
    
        public csUserController(IUserService service, ILogger<csUserController> logger)
        {
            _service = service;
            _logger = logger;
        }

    }
}