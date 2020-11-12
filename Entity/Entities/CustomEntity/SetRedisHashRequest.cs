using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Entities.CustomEntity
{
    public class SetRedisHashRequest
    {
        public string hashKey { get; set; }
        public string key { get; set; }
        public string value { get; set; }
    }
}
