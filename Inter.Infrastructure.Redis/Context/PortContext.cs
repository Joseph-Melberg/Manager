using StackExchange.Redis;
namespace Inter.Infrastructure.Redis.Context
{
    public class PortContext
    {

        private readonly IDatabase _database;
        public PortContext()
        {
            ConnectionMultiplexer muxer = ConnectionMultiplexer.Connect("hostname:port,password=password");
            _database = muxer.GetDatabase();
        }
    }
}
