using Microsoft.EntityFrameworkCore;

namespace EFCoreSampleApp
{
   public class ApplicationContext : DbContext
   {
      private readonly string _databasePath;

      internal ApplicationContext(string databasePath) => _databasePath = databasePath;

      public DbSet<Friend> Friends { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
         optionsBuilder.UseSqlite($"Filename={_databasePath}");
   }
}