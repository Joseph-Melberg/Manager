using System;
namespace Inter.Infrastructure.MySQL
{
    public interface IMySQLConnectionStringProvider
    {
        string GetConnectionString(string contextName);
    }
}
