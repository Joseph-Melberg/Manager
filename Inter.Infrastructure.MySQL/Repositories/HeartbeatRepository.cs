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

        public async Task<List<Heartbeat>> GetStatusesAsync() => await Context.Heartbeat.Select(_ => _.ToDomain()).ToListAsync();

        public async Task<Heartbeat> GetStatusAsync(string name) => (await Context.Heartbeat.FirstOrDefaultAsync(_ => _.name == name)).ToDomain();


        public async Task UpdateAsync(Heartbeat heartbeat)
        {
            var heartbeatModel = heartbeat.ToModel();
            if (Context.Heartbeat.Any(_ => _.name == heartbeatModel.name))
            {
                try
                {
                    Context.Heartbeat.Update(heartbeatModel);
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
                    await Context.Heartbeat.AddAsync(heartbeatModel);

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
