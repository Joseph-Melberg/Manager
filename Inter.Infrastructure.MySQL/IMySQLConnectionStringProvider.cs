using System;
namespace Inter.Infrastructure.MySQL
{
    public interface IMySQLConnectionStringProvider
    {
        string ConnectionString(string contextName);
    }
}
