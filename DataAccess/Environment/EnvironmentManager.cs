using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Environment
{
    public class EnvironmentManager
    {
        private static volatile EnvironmentManager _environmentManager;
        private static object lockObject = new object();

        public EnvironmentManager()
        {
            Get_EnvironmentName();
        }

        private IConfiguration configuration = null;
        private string environmentName = "";

        public static EnvironmentManager Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (_environmentManager == null)
                        _environmentManager = new EnvironmentManager();
                    return _environmentManager;
                }
            }
        }

        public string Get_EnvironmentName()
        {
            if (String.IsNullOrEmpty(environmentName))
            {
                try
                {
                    environmentName = System.Environment.GetEnvironmentVariable("R_ENVIRONMENT").ToLower();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            return environmentName;
        }

        public IConfiguration Create_Configuration()
        {
            if (configuration == null)
            {
                var builder = new ConfigurationBuilder().AddJsonFile($"{Get_EnvironmentName()}.json", true, true);
                configuration = builder.Build();
            }
            return configuration;
        }

        public IConfiguration Get_Configuration()
        {
            configuration = Create_Configuration();
            return configuration;
        }

        public bool Is_Development() => environmentName == "development" ? true : false;

        public bool Is_Staging() => environmentName == "staging" ? true : false;

        public bool Is_Production() => environmentName == "production" ? true : false;
    }
}
