using System;
using System.Threading.Tasks;
using Inter.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inter.Infrastructure.MySQL.Contexts
{
    public class LogContext : DefaultContext
    {
        public DbSet<LogModel> Log { get; set; }
        
        public LogContext(IMySQLConnectionStringProvider provider) : base(provider)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LogModel>(entity =>
            {
                entity.Property(_ => _.Severity);
                entity.Property(_ => _.Title);
                entity.Property(_ => _.Timestamp);
                entity.Property(_ => _.DeviceName);
                entity.Property(_ => _.ProcessName);
                entity.Property(_ => _.FormattedMessage);
                entity.Property(_ => _.MAC);
            });
        }
        public async Task Save()
        {
            await SaveChangesAsync();
        }
    }
}
