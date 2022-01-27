using InstagramApp.Services;
using Xamarin.Forms.Xaml;

namespace InstagramApp.Pages
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class UserProfilePage
   {
      private readonly UserService _service = new UserService();

      public UserProfilePage() => InitializeComponent();

      public UserProfilePage(int userId) : this() => BindingContext = _service.GetUser(userId);
   }
}