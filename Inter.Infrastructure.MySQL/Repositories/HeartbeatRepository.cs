using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Inter.Infrastructure.MySQL.Repositories
{
    public class HeartbeatRepository : BaseRepository<HeartbeatContext>, IHeartbeatRepository
    {

        public HeartbeatRepository(HeartbeatContext heartBeatContext) : base(heartBeatContext)
        {
            
        }

        public async Task<List<HeartbeatModel>> GetStatusesAsync()
        {
            return await Context.HeartBeat.ToListAsync();
        }

        public async Task<HeartbeatModel> GetStatusAsync(string name)
        {
             return await Context.HeartBeat.FirstOrDefaultAsync(_ => _.name == name);
        }

        public async Task UpdateAsync(HeartbeatModel heartBeat)
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
                    await Context.HeartBeat.AddAsync(heartBeat);

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