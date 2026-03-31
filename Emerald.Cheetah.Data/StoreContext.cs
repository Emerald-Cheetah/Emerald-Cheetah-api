using Emerald.Cheetah.Domain.Catalog;
using Microsoft.EntityFrameworkCore;

namespace Emerald.Cheetah.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) 
            : base(options)
        { }

        public DbSet<Item> Items { get; set; }
    }
}