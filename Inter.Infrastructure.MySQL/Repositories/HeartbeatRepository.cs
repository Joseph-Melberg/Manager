using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.Mappers;
using Inter.Infrastructure.MySQL.Contexts;
using Melberg.Infrastructure.MySql;
using Microsoft.EntityFrameworkCore;

namespace Inter.Infrastructure.MySQL.Repositories
{
    public class HeartbeatRepository : BaseRepository<HeartbeatContext>, IHeartbeatRepository
    {

        public HeartbeatRepository(HeartbeatContext heartBeatContext) : base(heartBeatContext)
        {
            
        }

        public async Task<List<Heartbeat>> GetStatiAsync()
        {
            return await Context.HeartBeat.Select(_ => _.ToDomain()).ToListAsync();
        }

        public async Task<Heartbeat> GetStatusAsync(string name)
        {
            return (await Context.HeartBeat.FirstOrDefaultAsync(_ => _.name == name)).ToDomain();
        }


        public async Task UpdateAsync(Heartbeat heartBeat)
        {

            if (Context.HeartBeat.Any(_ => _.name == heartBeat.name))
            {
                try
                {
                    Context.HeartBeat.Update(heartBeat.ToModel());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            else
            {
                try
                {
                    await Context.HeartBeat.AddAsync(heartBeat.ToModel());
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
