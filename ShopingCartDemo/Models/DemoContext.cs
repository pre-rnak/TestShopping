using System.Data.Entity;

namespace ShopingCartDemo.Models
{
    public class DemoContext : DbContext
    {
        public DemoContext()
        {
            Database.SetInitializer<DemoContext>(null);
        }
        public DbSet<Item> Item { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Login> Login { get; set; }
    }
}