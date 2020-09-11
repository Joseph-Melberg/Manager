using System;
using OnlineOfflineReaderService.Domain;
using Microsoft.EntityFrameworkCore;

namespace OnlineOfflineReaderService.Infrastructure.MySql
{
    public class HeartBeatContext : DbContext
    {
        public DbSet<HeartBeatModel> Temperature { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=10.0.0.3;database=Inter;user=user;password=pass");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HeartBeatModel>(entity =>
            {
                entity.HasKey(_ => _.name);
                entity.Property(_ => _.timestamp);
            });
        }
    }
}
