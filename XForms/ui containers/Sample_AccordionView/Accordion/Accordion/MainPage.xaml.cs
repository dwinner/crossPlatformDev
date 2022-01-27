using System.ComponentModel;
using Xamarin.Forms;

namespace Accordion
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class MainPage : ContentPage
   {
      public MainPage()
      {
         InitializeComponent();
      }
   }

   public class Todo
   {
      public bool Done { get; set; }

      public string Title { get; set; }
   }
}