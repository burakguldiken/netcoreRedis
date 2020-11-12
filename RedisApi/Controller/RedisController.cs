using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Constants;
using Business.Enum;
using Business.Interfaces;
using Entity.Entities.CustomEntity;
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
        /// Set redis hash
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("redissethash")]
        public IActionResult Set_Redis_Hash(SetRedisHashRequest request)
        {
            _sRedis.Set_Hash(request.hashKey, request.key, request.value);
            return Ok(Message.addedRedisDatabase);
        }

        /// <summary>
        /// Delete redis hash
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("redisdeletehash")]
        public IActionResult Delete_Redis_Hash(DeleteRedisHashRequest request)
        {
            _sRedis.Delete_Hash(request.hashKey, request.key);
            return Ok(Message.deletedRedisDatabase);
        }

        /// <summary>
        /// Get redis hash
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("redisgethash")]
        public IActionResult Get_Redis_Hash(GetRedisHashRequest request)
        {
            var response = _sRedis.Get_Hash<SetRedisHashRequest>(request.hashKey, request.key);
            return Ok(response);
        }

        /// <summary>
        /// Set redis
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("redisset")]
        public IActionResult Set_Redis(SetRedisRequest request)
        {
            _sRedis.Set(request.key, request.value,null,(int)enumDatabase.redisDatabase);
            return Ok(Message.addedRedisDatabase);
        }


        /// <summary>
        /// Get redis
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("redisset")]
        public IActionResult Get_Redis(GetRedisRequest request)
        {
            var response = _sRedis.Get(request.key);
            return Ok(response);
        }

    }
}
