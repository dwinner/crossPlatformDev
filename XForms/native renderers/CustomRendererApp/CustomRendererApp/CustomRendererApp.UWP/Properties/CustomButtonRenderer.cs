using System.ComponentModel;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using CustomRendererApp;
using CustomRendererApp.UWP.Properties;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using ContentPresenter = Windows.UI.Xaml.Controls.ContentPresenter;
using CornerRadius = Windows.UI.Xaml.CornerRadius;
using Thickness = Windows.UI.Xaml.Thickness;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]

namespace CustomRendererApp.UWP.Properties
{
   public class CustomButtonRenderer : ViewRenderer<CustomButton, Border>
   {
      protected override void OnElementChanged(ElementChangedEventArgs<CustomButton> e)
      {
         base.OnElementChanged(e);

         if (Control == null)
         {
            var buttonBorder = new Border
            {
               CornerRadius = new CornerRadius(25.0),
               Padding = new Thickness(4),
               Background = new SolidColorBrush(Colors.LightBlue),
               BorderBrush = new SolidColorBrush(Colors.CadetBlue),
               BorderThickness = new Thickness(2)
            };

            var contentPresenter = new ContentPresenter
            {
               VerticalAlignment = VerticalAlignment.Center,
               HorizontalAlignment = HorizontalAlignment.Center
            };
            buttonBorder.Child = contentPresenter;
            buttonBorder.Tapped += (sender, args) => ((IButtonController) Element).SendClicked();
            SetNativeControl(buttonBorder);
            if (e.NewElement != null)
            {
               SetText();
            }
         }
      }

      protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
      {
         base.OnElementPropertyChanged(sender, e);

         if (e.PropertyName == HeaderView.TextProperty.PropertyName)
         {
            SetText();
         }
      }

      private void SetText()
      {
         if (Control.Child is ContentPresenter contentPresenter)
         {
            contentPresenter.Content = Element.Text;
         }
      }
   }
}