using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace EFCoreSampleApp
{
   public partial class App
   {
      public const string DbFileName = "friendsapp.db";

      public App()
      {
         InitializeComponent();

         var dbPath = DependencyService.Get<IDbPath>().GetDbPath(DbFileName);
         using (var appContext = new ApplicationContext(dbPath))
         {
            SQLitePCL.Batteries.Init();
            appContext.Database.EnsureCreated();
            if (!appContext.Friends.Any())
            {
               appContext.Friends.Add(new Friend
               {
                  Name = "Tom",
                  Email = "tom@gmail.com",
                  Phone = "+1234567"
               });
               appContext.Friends.Add(new Friend
               {
                  Name = "Alice",
                  Email = "alice@gmail.com",
                  Phone = "+3435957"
               });
               appContext.SaveChanges();
            }
         }

         MainPage = new NavigationPage(new MainPage());
      }

      protected override void OnStart()
      {
         // Handle when your app starts
      }

      protected override void OnSleep()
      {
         // Handle when your app sleeps
      }

      protected override void OnResume()
      {
         // Handle when your app resumes
      }
   }
}