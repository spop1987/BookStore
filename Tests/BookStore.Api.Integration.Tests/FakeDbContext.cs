using BookStore.DataStore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Integration.Tests
{
    public class FakeDbContext : BookStoreContext
    {
        public FakeDbContext(DbContextOptions<BookStoreContext> options) : base(options)
        {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}