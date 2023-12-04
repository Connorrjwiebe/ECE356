using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Persistence.Contexts
{
	public class AppDbContext : DbContext
	{
		public DbSet<Sensor> test356 { get; set; }
		//public DbSet<Product> Products { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
    
    builder.Entity<Sensor>().ToTable("test356");
    builder.Entity<Sensor>().HasKey(p => p.Time);
    builder.Entity<Sensor>().Property(p => p.Time).IsRequired().ValueGeneratedOnAdd().HasValueGenerator<InMemoryIntegerValueGenerator<int>>();
    builder.Entity<Sensor>().Property(p => p.Ph).IsRequired();
	builder.Entity<Sensor>().Property(p => p.Conductivity).IsRequired();
	builder.Entity<Sensor>().Property(p => p.Temperature).IsRequired();

		}
	}
}