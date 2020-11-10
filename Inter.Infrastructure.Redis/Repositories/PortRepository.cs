using System;
using StackExchange.Redis;

namespace Inter.Infrastructure.Redis.Repositories
{
    public class PortRepository
    {
        private readonly IDatabaseAsync _database;

        public PortRepository()
        {
            ConnectionMultiplexer muxer = ConnectionMultiplexer.Connect("10.0.0.104:6379,password=distrust");
            _database = muxer.GetDatabase();
        }
        public void GetAll
        public void SetPort(string name, int port)
        {
            _database.
        }
    }
}
