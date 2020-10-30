using System.Threading.Tasks;
using Inter.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inter.Infrastructure.MySQL.Contexts
{
    public interface ILogContext
    {
        public DbSet<LogModel> Log { get; set; }
        Task Save();
    }
}
