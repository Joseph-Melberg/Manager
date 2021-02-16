using Inter.Infrastructure.MySQL.Models;
using Microsoft.EntityFrameworkCore;

namespace Inter.Infrastructure.MySQL.Contexts
{
    public class PlaneContext : DefaultContext
    {
        public DbSet<PlaneModel> records { get; set; }

        public PlaneContext(IMySQLConnectionStringProvider provider) : base(provider)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PlaneModel>(entity =>
                           {
                               entity.HasKey(_ => _.id);
                           });
        }
    }
}