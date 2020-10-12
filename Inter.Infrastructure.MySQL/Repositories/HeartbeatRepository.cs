using System;
using System.Linq;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL.Contexts;

namespace Inter.Infrastructure.MySQL.Repositories
{
    public class HeartbeatRepository : IHeartbeatRepository
    {
        private readonly IHeartbeatContext _heartBeatContext;

        public HeartbeatRepository(IHeartbeatContext heartBeatContext)
        {
            _heartBeatContext = heartBeatContext;
        }

        public HeartbeatModel[] GetStatuses()
        {
            return _heartBeatContext.HeartBeat.ToArray();
        }

        public HeartbeatModel GetStatus(string name)
        {
             return _heartBeatContext.HeartBeat.FirstOrDefault(_ => _.name == name);
        }

        public Task UpdateAsync(HeartbeatModel heartBeat)
        {

            if (_heartBeatContext.HeartBeat.Any(_ => _.name == heartBeat.name))
            {
                try
                {
                    _heartBeatContext.HeartBeat.Update(heartBeat);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            else
            {
                Console.WriteLine($"Node {heartBeat.name} was added");
                try
                {
                    _heartBeatContext.HeartBeat.Add(heartBeat);

                }
                catch ( Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return _heartBeatContext.Save();
        }
    }
}