using Inter.Infrastructure.MySQL.Configurations;
using Inter.Infrastructure.MySQL.Models;
using MelbergFramework.Core.MySql;
using MelbergFramework.Infrastructure.MySql;
using Microsoft.EntityFrameworkCore;

namespace Inter.Infrastructure.MySQL.Contexts;
public class ReadWriteContext : DefaultContext
{
    public DbSet<HeartbeatModel> Heartbeats {get; set;}
    public DbSet<PlaneFrameMetadataModel> PlaneFrameMetadata {get; set;}
    public ReadWriteContext(IMySqlConnectionStringProvider provider) : base(provider)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new HeartbeatModelConfiguration());
        modelBuilder.ApplyConfiguration(new PlaneFrameMetadataModelConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}