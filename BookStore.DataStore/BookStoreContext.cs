using BookStore.Models.Entities;
using BookStore.Models.Entities.TypeConfig;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataStore
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(IsDebugMode())
                optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        private bool IsDebugMode()
        {
            var env = Environment.GetEnvironmentVariable("DEBUG_MODE");
            if(env == "true")
                return true;

            return false;
        }

        public bool IsPersonUpdated { get; internal set; }

        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS").ConfigureAllTypeConfig();
        }

        public async Task<int> SaveChangesAsync(string username, string source)
        {
            await AddLogInfo(username, source);
            return await base.SaveChangesAsync();
        }

        public int SaveChanges(string username, string source)
        {
            AddLogInfo(username, source).Wait();
            return base.SaveChanges();
        }

        private async Task AddLogInfo(string username, string source)
        {
            IsPersonUpdated = false;
            foreach (var entity in ChangeTracker.Entries().ToList())
            {
                if(entity.Entity is EntityLogBase)
                {
                    var now = DateTime.Now;
                    if(entity.State == EntityState.Added)
                    {
                        ((EntityLogBase)entity.Entity).CreatedDate = now;
                        ((EntityLogBase)entity.Entity).CreateBy = username;
                        ((EntityLogBase)entity.Entity).CreatedSystem = source;
                        IsPersonUpdated = true;
                    }
                    else if(entity.State == EntityState.Modified)
                    {
                        ((EntityLogBase)entity.Entity).UpdatedDate = now;
                        ((EntityLogBase)entity.Entity).UpdatedBy = username;
                        ((EntityLogBase)entity.Entity).UpdatedSystem = source;
                        IsPersonUpdated = true;
                    }
                }
            }
        }
    }
}