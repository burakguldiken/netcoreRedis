using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Entities.CustomEntity
{
    public class GetRedisHashRequest
    {
        public string hashKey { get; set; }
        public string key { get; set; }
    }
}
