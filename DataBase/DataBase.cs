using Microsoft.EntityFrameworkCore;

namespace sequorProduction.DataContext{
public class DataBase : DbContext
{
    public DbSet<Material> Material { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Production> Production { get; set; }
    public DbSet<ProductMaterial> ProductMaterial { get; set; }
    public DbSet<User> User { get; set; }

    public DataBase(DbContextOptions<DataBase> options) : base(options)
    {
        Material = Set<Material>();
        Order = Set<Order>();
        Product = Set<Product>();
        Production = Set<Production>();
        ProductMaterial = Set<ProductMaterial>();
        User = Set<User>();
    }
}


}
