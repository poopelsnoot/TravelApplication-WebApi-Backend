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
using Models.DTO;

namespace AppWebbApi.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class csCommentController: Controller
    {
        private ILogger<csCommentController> _logger = null;
        private ICommentService _service = null;
        
        //endpoint to read all comments for a specified attraction
        [HttpGet()]
        [ActionName("ReadComments")]
        [ProducesResponseType(200, Type = typeof(List<IComment>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadComments(string attractionId)
        {
            try
            {
                _logger.LogInformation("Endpoint ReadComments executed");
                Guid _id = Guid.Parse(attractionId);
                var comments = _service.ReadComments(_id);
                
                return Ok(comments);
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint ReadComments error");
                return BadRequest(ex.Message);
            }
            
        }

        //endpoint to read a specified comment in DTO format 
        [HttpGet()]
        [ActionName("ReadCommentDto")]
        [ProducesResponseType(200, Type = typeof(csCommentCUdto))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadCommentDto(string CommentId = null)
        {
            try
            {
                _logger.LogInformation("Endpoint ReadCommentDto executed");
                var _id = Guid.Parse(CommentId);
                
                var item = _service.ReadComment(_id);
                if (item == null)
                {
                    return BadRequest($"Item with id {CommentId} does not exist");
                }

                var dto = new csCommentCUdto(item);
                return Ok(dto);          
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint ReadCommentDto error");
                return BadRequest(ex.Message);
            }
        }

        //endpoint to add a new comment
        [HttpPost()]
        [ActionName("AddComment")]
        [ProducesResponseType(200, Type = typeof(IComment))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> AddComment([FromBody] csCommentCUdto item)
        {
            try
            {
                _logger.LogInformation("Endpoint AddComment executed");

                var _item = _service.AddComment(item);
                _logger.LogInformation($"item {_item.CommentId} created");

                return Ok(_item);      
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint AddComment error");
                return BadRequest(ex.Message);
            }
        }

        //endpoint to remove an existing comment
        [HttpDelete()]
        [ActionName("RemoveComment")]
        [ProducesResponseType(200, Type = typeof(List<IComment>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> RemoveComment(string Id)
        {
            try
            {
                _logger.LogInformation("Endpoint RemoveComment executed");
                Guid _id = Guid.Parse(Id);
                adminInfoDbDto _info = _service.RemoveComment(_id);
                
                return Ok(_info);
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint RemoveComment error");
                return BadRequest(ex.Message);
            }
            
        }
    
        //constructor that injects logger and service
        public csCommentController(ICommentService service, ILogger<csCommentController> logger)
        {
            _service = service;
            _logger = logger;
        }

    }
}