using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Persistence.Contexts.Configurations
{
    public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(p => p.Time);
            builder.Property(p => p.Time).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.Ph).IsRequired();
            builder.Property(p => p.Conductivity).IsRequired();
            builder.Property(p => p.Temperature).IsRequired();
           
        }
    }
}