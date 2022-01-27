using InstagramApp.Models;
using InstagramApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InstagramApp.Pages
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class ActivityFeedPage
   {
      private readonly ActivityService _service = new ActivityService();

      public ActivityFeedPage()
      {
         InitializeComponent();
         activityFeed.ItemsSource = _service.GetActivities();
      }

      private void OnActivitySelected(object sender, SelectedItemChangedEventArgs e)
      {
         if (e.SelectedItem is Activity activity)
         {
            activityFeed.SelectedItem = null;
            Navigation.PushAsync(new UserProfilePage(activity.UserId));
         }
      }
   }
}