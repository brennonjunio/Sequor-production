using Microsoft.EntityFrameworkCore;

namespace sequorProduction.DataContext
{
    public class DataBase : DbContext
    {
        public DbSet<MaterialModel> Material { get; set; }
        public DbSet<OrderModel> Order { get; set; }
        public DbSet<ProductModel> Product { get; set; }
        public DbSet<ProductionModel> Production { get; set; }
        public DbSet<ProductMaterial> ProductMaterial { get; set; }
        public DbSet<UserModel> User { get; set; }

        public DataBase(DbContextOptions<DataBase> options) : base(options)
        {
            Material = Set<MaterialModel>();
            Order = Set<OrderModel>();
            Product = Set<ProductModel>();
            Production = Set<ProductionModel>();
            ProductMaterial = Set<ProductMaterial>();
            User = Set<UserModel>();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductMaterial>()
                .HasKey(pm => new { pm.ProductCode, pm.MaterialCode });

            modelBuilder.Entity<ProductModel>()
           .Property(p => p.cycleTime)
           .HasDefaultValue(18.2);

           modelBuilder.Entity<ProductionModel>()
           .Property(p => p.quantity)
           .HasDefaultValue(18.2);

            modelBuilder.Entity<ProductionModel>()
           .Property(p => p.cycleTime)
           .HasDefaultValue(18.2);
        }

    }

}
