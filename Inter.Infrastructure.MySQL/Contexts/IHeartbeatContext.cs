using System.Threading.Tasks;
using Inter.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inter.Infrastructure.MySQL.Contexts
{
    public interface IHeartbeatContext
    {
        DbSet<HeartbeatModel> HeartBeat { get; set; }
        Task Save();
    }
}
