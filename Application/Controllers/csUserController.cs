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
    public class csUserController: Controller
    {
        private ILogger<csUserController> _logger = null;
        private IUserService _service = null;
        
        //endpoint that reads all users
        [HttpGet()]
        [ActionName("ReadUsers")]
        [ProducesResponseType(200, Type = typeof(List<IUser>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadUsers()
        {
            try
            {
                _logger.LogInformation("Endpoint ReadUsers executed");
                var users = _service.ReadUsers();
                
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint ReadUsers error");
                return BadRequest(ex.Message);
            }
            
        }

        //endpoint that reads a specified user in DTO format
        [HttpGet()]
        [ActionName("ReadUserDto")]
        [ProducesResponseType(200, Type = typeof(csUserCUdto))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadUserDto(string UserId = null)
        {
            try
            {
                _logger.LogInformation("Endpoint ReadUserDto executed");
                var _id = Guid.Parse(UserId);
                
                var item = _service.ReadUser(_id);
                if (item == null)
                {
                    return BadRequest($"Item with id {UserId} does not exist");
                }

                var dto = new csUserCUdto(item);
                return Ok(dto);          
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint ReadUserDto error");
                return BadRequest(ex.Message);
            }
        }

        //endpoint that adds a new user
        [HttpPost()]
        [ActionName("AddUser")]
        [ProducesResponseType(200, Type = typeof(IUser))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> AddUser([FromBody] csUserCUdto item)
        {
            try
            {
                _logger.LogInformation("Endpoint AddUser executed");

                var _item = _service.AddUser(item);
                _logger.LogInformation($"item {_item.UserId} created");

                return Ok(_item);      
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint AddUser error");
                return BadRequest(ex.Message);
            }
        }

        //endpoint that removes an existing user
        [HttpDelete()]
        [ActionName("RemoveUser")]
        [ProducesResponseType(200, Type = typeof(List<IUser>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> RemoveUser(string Id)
        {
            try
            {
                _logger.LogInformation("Endpoint RemoveUser executed");
                Guid _id = Guid.Parse(Id);
                adminInfoDbDto _info = _service.RemoveUser(_id);
                
                return Ok(_info);
            }
            catch (Exception ex)
            {
                _logger.LogError("Endpoint RemoveUser error");
                return BadRequest(ex.Message);
            }
            
        }

        //constructor that injects logger and service
        public csUserController(IUserService service, ILogger<csUserController> logger)
        {
            _service = service;
            _logger = logger;
        }

    }
}