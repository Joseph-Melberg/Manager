﻿using System.Threading.Tasks;
using Inter.Domain;
using Melberg.Infrastructure.MySql;
using Microsoft.EntityFrameworkCore;

namespace Inter.Infrastructure.MySQL.Contexts
{
    public class HeartbeatContext : DefaultContext
    {
        public DbSet<HeartbeatModel> HeartBeat { get; set; }
        public HeartbeatContext(IMySQLConnectionStringProvider provider) : base(provider)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HeartbeatModel>(entity =>
            {

                entity.HasKey(_ => _.name);
                entity.Property(_ => _.timestamp);
                entity.Property(_ => _.online);
                entity.Property(_ => _.announced);
                entity.Property(_ => _.mac);
            });
        }
        public async Task SaveAsync()
        {
            await this.SaveChangesAsync();
        }
    }
}
