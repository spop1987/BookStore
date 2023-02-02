using Microsoft.EntityFrameworkCore;

namespace BookStore.Models.Entities.TypeConfig
{
    public static class Extensions
    {
        public static void ConfigureAllTypeConfig(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonTypeConfig());
        }
    }
}