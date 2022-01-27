using Microsoft.EntityFrameworkCore;

namespace SocialService.Models
{
   public class ApplicationContext : DbContext
   {
      private const string DefaultConnectionString =
         @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\private\MobileDev\Xamarin\SocialService\SocialService\Social.mdf;Integrated Security=True";

      public DbSet<Friend> Friends { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         if (!optionsBuilder.IsConfigured)
         {
            optionsBuilder.UseSqlServer(DefaultConnectionString);
         }
      }
   }
}