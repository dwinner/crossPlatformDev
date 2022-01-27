using Microsoft.EntityFrameworkCore;
using MvvxSandboxApp.Core.Models;

namespace MvvxSandboxApp.Core.Services.Implementations
{
   internal sealed class UsaStateDbContext : DbContext
   {
      private readonly string _dbPath;

      public UsaStateDbContext(string dbPath)
      {
         _dbPath = dbPath;
         Database.EnsureCreated();
      }

      public DbSet<UsaState> UsaStates { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         if (!optionsBuilder.IsConfigured)
         {
            optionsBuilder
               //.UseLazyLoadingProxies() #BUG For now there are a lot of issues with lazy loading in SqLite
               .UseSqlite($"Filename={_dbPath}");
            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
         }
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.ApplyConfiguration(new UsaStateEntityConfig());
         modelBuilder.ApplyConfiguration(new UsaStateDetailEntityConfig());

         base.OnModelCreating(modelBuilder);
      }
   }
}