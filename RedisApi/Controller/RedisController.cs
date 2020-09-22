using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RedisApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private IRedis _sRedis;
        
        public RedisController(IRedis sRedis)
        {
            _sRedis = sRedis;
        }

        /// <summary>
        /// Get user by id from redis database 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getuserfromredis/{id:long}")]
        public IActionResult Get_Redis_User(long id)
        {
            return Ok("");
        }
    }
}
