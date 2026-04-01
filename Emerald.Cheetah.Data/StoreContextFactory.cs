using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Emerald.Cheetah.Data
{
    public class StoreContextFactory : IDesignTimeDbContextFactory<StoreContext>
    {
        public StoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();
            optionsBuilder.UseSqlite(
                "Data Source=Registrar.sqlite",
                b => b.MigrationsAssembly("Emerald.Cheetah.Api")
            );

            return new StoreContext(optionsBuilder.Options);
        }
    }
}