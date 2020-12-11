using System;
using System.Linq;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL.Contexts;

namespace Inter.Infrastructure.MySQL.Repositories
{
    public class HeartbeatRepository : BaseRepository<HeartbeatContext>, IHeartbeatRepository
    {

        public HeartbeatRepository(HeartbeatContext heartBeatContext) : base(heartBeatContext)
        {
            
        }

        public HeartbeatModel[] GetStatuses()
        {
            return Context.HeartBeat.ToArray();
        }

        public HeartbeatModel GetStatus(string name)
        {
             return Context.HeartBeat.FirstOrDefault(_ => _.name == name);
        }

        public Task UpdateAsync(HeartbeatModel heartBeat)
        {

            if (Context.HeartBeat.Any(_ => _.name == heartBeat.name))
            {
                try
                {
                    Context.HeartBeat.Update(heartBeat);
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
                    Context.HeartBeat.Add(heartBeat);

                }
                catch ( Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return Context.Save();
        }
    }
}