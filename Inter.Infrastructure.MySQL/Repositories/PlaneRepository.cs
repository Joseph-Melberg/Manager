using Inter.Domain;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.Mappers;
using Inter.Infrastructure.MySQL.Contexts;
using Melberg.Infrastructure.MySql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

using System.Threading.Tasks;

namespace Inter.Infrastructure.MySQL.Repositories
{
    public class PlaneRepository : BaseRepository<PlaneContext>, IPlaneRepository
    {
        public PlaneRepository(PlaneContext context) : base(context)
        {
        }
        public async Task AddPlaneAsync(Plane plane,int now, DateTime time)
        {
            await Context.records.AddAsync(plane.ToModel(now, time));
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
        public async Task<int> PlaneCount()
        {
            var result = await Context.records.OrderByDescending(_ => _.id).FirstOrDefaultAsync();
            return result?.id ?? -1;
        }
    }
}