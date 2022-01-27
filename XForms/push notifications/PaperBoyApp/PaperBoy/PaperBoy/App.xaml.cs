using PaperBoy.ViewModels;
using Xamarin.Forms;

namespace PaperBoy
{
   //android Package name : com.paperboy.android
   public partial class App : Application
   {
      //private static FavoritesDatabase database;
      //public static FavoritesDatabase Database
      //{
      //    get
      //    {
      //        if(database==null)
      //        {
      //            database=new FavoritesDatabase(StorageHelper.GetLocalFilePath("Favorite.db"));
      //        }
      //        return database;
      //    }
      //}
      public App()
      {
         InitializeComponent();

         MainPage = new NavigationPage(new MainPage());
      }

      public static MainViewModel ViewModel { get; set; }
      public static INavigation MainNavigation { get; set; }

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