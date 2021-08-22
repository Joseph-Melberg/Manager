using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL.Contexts;
using Inter.Infrastructure.MySQL.Mappers;
using Melberg.Infrastructure.MySql;
using Microsoft.EntityFrameworkCore;

namespace Inter.Infrastructure.MySQL.Repositories
{
    public class HeartbeatRepository : BaseRepository<HeartbeatContext>, IHeartbeatRepository
    {

        public HeartbeatRepository(HeartbeatContext heartBeatContext) : base(heartBeatContext)
        {
            
        }

        public async Task<IList<Heartbeat>> GetStatusesAsync() => await Context.HeartBeat.Select(_ => _.ToDomain()).ToListAsync();

        public async Task<Heartbeat> GetStatusAsync(string name) => (await Context.HeartBeat.FirstOrDefaultAsync(_ => _.name == name)).ToDomain();


        public async Task UpdateAsync(Heartbeat heartbeat)
        {
            var heartbeatModel = heartbeat.ToModel();
            if (Context.HeartBeat.Any(_ => _.name == heartbeatModel.name))
            {
                try
                {
                    Context.HeartBeat.Update(heartbeatModel);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            else
            {
                Console.WriteLine($"Node {heartbeatModel.name} was added");
                try
                {
                    await Context.HeartBeat.AddAsync(heartbeatModel);

                }
                catch ( Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            await Context.SaveAsync();
        }
    }
}
