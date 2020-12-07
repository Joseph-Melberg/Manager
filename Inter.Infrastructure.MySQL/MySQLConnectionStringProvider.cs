using System;
using Microsoft.Extensions.Configuration;
namespace Inter.Infrastructure.MySQL
{
    public class MySQLConnectionStringProvider : IMySQLConnectionStringProvider
    {
        private readonly IConfiguration _configuration;

        public MySQLConnectionStringProvider(IConfiguration configRoot)
        {
            _configuration = configRoot;
        }

        public string ConnectionString(string contextName)
        {
            return _configuration.GetConnectionString(contextName);
        }
    }
}
