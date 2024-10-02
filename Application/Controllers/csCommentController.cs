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
    public class csCommentController: Controller
    {
        private ILogger<csCommentController> _logger = null;
        private ICommentService _service = null;
        
        [HttpGet()]
        [ActionName("ReadComments")]
        [ProducesResponseType(200, Type = typeof(List<IComment>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadComments(string attractionId)
        {
            try
            {
                _logger.LogInformation("Endpoint ListAttraction executed");
                Guid _id = Guid.Parse(attractionId);
                var comments = _service.ReadComments(_id);
                
                return Ok(comments);
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint ListAttraction error");
                return BadRequest(ex.Message);
            }
            
        }
    
        public csCommentController(ICommentService service, ILogger<csCommentController> logger)
        {
            _service = service;
            _logger = logger;
        }

    }
}