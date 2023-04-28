using System.Threading.Tasks;
using Inter.Infrastructure.MySQL.Models;
using MelbergFramework.Core.MySql;
using MelbergFramework.Infrastructure.MySql;
using Microsoft.EntityFrameworkCore;

namespace Inter.Infrastructure.MySQL.Contexts;
public class TemperatureContext : DefaultContext
{

    public DbSet<TemperatureMarkModel> Temperature { get; set; }

    public TemperatureContext(IMySqlConnectionStringProvider provider) : base(provider)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<TemperatureMarkModel>(entity =>
                       {
                           entity.HasKey(_ => _.id);
                           entity.Property(_ => _.hostname);
                           entity.Property(_ => _.specific);
                           entity.Property(_ => _.temperature);
                           entity.Property(_ => _.timestamp);
                       });
    }
    
    public async Task SaveAsync()
    {
        await this.SaveChangesAsync();
    }
}