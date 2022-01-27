using Xamarin.Forms.Xaml;

namespace IntroducingPages
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class MasterDetailSample
   {
      public MasterDetailSample()
      {
         InitializeComponent();

         // Use IsPresented to programmatically control
         // the visibility of the Master
         IsPresented = true;
      }
   }
}