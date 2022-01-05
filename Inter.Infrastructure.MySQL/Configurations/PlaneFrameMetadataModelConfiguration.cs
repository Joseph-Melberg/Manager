using Inter.Infrastructure.MySQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inter.Infrastructure.MySQL.Configurations
{
    public class PlaneFrameMetadataModelConfiguration : IEntityTypeConfiguration<PlaneFrameMetadataModel>
    {
        public void Configure(EntityTypeBuilder<PlaneFrameMetadataModel> builder)
        {
            builder.ToTable("PlaneFrameMetadata");
            builder.HasKey(_ => _.id);
        }
    }
}