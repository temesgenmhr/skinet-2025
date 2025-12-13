namespace Infrastructure.Data
{
    using Core.Entities;
    using Infrastructure.Config;
    using Microsoft.EntityFrameworkCore;

    public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        }
    }
}