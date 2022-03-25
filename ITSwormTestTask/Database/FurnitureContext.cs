using Microsoft.EntityFrameworkCore;

namespace ITSwormTestTask.Database
{
    public class FurnitureContext : DbContext
    {
        public DbSet<Furniture> Furniture { get; set; }
        public DbSet<Panel> Panels { get; set; }
        public DbSet<Accessorie> Accessories { get; set; }
        public DbSet<Fastener> Fasteners { get; set; }

        public FurnitureContext(DbContextOptions<FurnitureContext> options)
        : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Furniture>()
                .HasMany(f => f.Panels)
                .WithOne(p => p.Furniture);

            modelBuilder.Entity<Panel>()
                .HasMany(p => p.Fasteners)
                .WithOne(f => f.Panel);

            modelBuilder.Entity<Panel>()
                .HasMany(p => p.Accessories)
                .WithOne(f => f.Panel);
        }
    }
}
