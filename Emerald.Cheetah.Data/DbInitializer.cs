using Emerald.Cheetah.Domain.Catalog;
using Microsoft.EntityFrameworkCore;

namespace Emerald.Cheetah.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ModelBuilder builder)
        {
            builder.Entity<Item>().HasData(
                new Item("Shirt", "Ohio State shirt", "Nike", 29.99m)
                {
                    Id = 1
                },
                new Item("Shorts", "Ohio State shorts", "Nike", 49.99m)
                {
                    Id = 2
                }
            );
        }
    }
}