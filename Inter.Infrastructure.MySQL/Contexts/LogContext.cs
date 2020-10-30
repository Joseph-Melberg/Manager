using System;
using System.Threading.Tasks;
using Inter.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inter.Infrastructure.MySQL.Contexts
{
    public class LogContext : DefaultContext, ILogContext
    {
        public DbSet<LogModel> Log { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=10.0.0.3;database=Inter;user=user;password=pass");
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
