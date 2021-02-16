using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using System.Threading.Tasks;

namespace Inter.Infrastructure.MySQL.Repositories
{
    public class PlaneRepository : BaseRepository<PlaneContext>, IPlaneRepository
    {
        public PlaneRepository(PlaneContext context) : base(context)
        {
        }
        public async Task<int> PlaneCount()
        {
            var result = await Context.records.OrderByDescending(_ => _.id).FirstOrDefaultAsync();
            return result?.id ?? -1;
        }
    }
}