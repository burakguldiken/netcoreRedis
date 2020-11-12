using DataAccess.Environment;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;


namespace DataAccess.Connections
{
    public class Connection
    {
        private static Connection _connection = null;

        public static Connection Instance
        {
            get
            {
                if (_connection == null)
                    _connection = new Connection();
                return _connection;
            }
        }

        public string redisIp { get; set; }
        public string redisPort { get; set; }
        public string redisHost { get; set; }

        public Connection()
        {
            EnvironmentManager environmentManager = EnvironmentManager.Instance;
            IConfiguration configuration = environmentManager.Get_Configuration();
            redisIp = "127.0.0.1";
            redisPort = "6379";
            redisHost = $"{redisIp}:{redisPort},password=";
        }
    }
}
