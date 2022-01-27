using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Accordion.CustomControls
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class Accordion : ContentView
   {
      public static readonly BindableProperty IndicatorViewProperty =
         BindableProperty.Create(nameof(IndicatorView), typeof(View), typeof(Accordion), default(View));

      public static readonly BindableProperty ContentViewProperty =
         BindableProperty.Create(nameof(AccordionContentView), typeof(View), typeof(Accordion), default(View));

      public static readonly BindableProperty TitleBindableProperty =
         BindableProperty.Create(nameof(Title), typeof(string), typeof(Accordion), default(string));

      public static readonly BindableProperty IsOpenBindablePropertyProperty = BindableProperty.Create(nameof(IsOpen),
         typeof(bool), typeof(Accordion), false, propertyChanged: IsOpenChanged);

      public static readonly BindableProperty HeaderBackgroundColorProperty =
         BindableProperty.Create(nameof(HeaderBackgroundColor), typeof(Color), typeof(Accordion), Color.Transparent);

      public Accordion()
      {
         InitializeComponent();
         Close();
         AnimationDuration = 250;
         IsOpen = false;
      }

      public View IndicatorView
      {
         get => (View) GetValue(IndicatorViewProperty);
         set => SetValue(IndicatorViewProperty, value);
      }

      public View AccordionContentView
      {
         get => (View) GetValue(ContentViewProperty);
         set => SetValue(ContentViewProperty, value);
      }

      public string Title
      {
         get => (string) GetValue(TitleBindableProperty);
         set => SetValue(TitleBindableProperty, value);
      }

      public bool IsOpen
      {
         get => (bool) GetValue(IsOpenBindablePropertyProperty);
         set => SetValue(IsOpenBindablePropertyProperty, value);
      }

      public Color HeaderBackgroundColor
      {
         get => (Color) GetValue(HeaderBackgroundColorProperty);
         set => SetValue(HeaderBackgroundColorProperty, value);
      }

      public uint AnimationDuration { get; set; }

      private static void IsOpenChanged(BindableObject bindable, object oldValue, object newValue)
      {
         bool isOpen;

         if (bindable != null && newValue != null)
         {
            var control = (Accordion) bindable;
            isOpen = (bool) newValue;

            if (control.IsOpen == false)
            {
               VisualStateManager.GoToState(control, "Open");
               control.Close();
            }
            else
            {
               VisualStateManager.GoToState(control, "Closed");
               control.Open();
            }
         }
      }

      private async void Close()
      {
         await Task.WhenAll(
            accContent.TranslateTo(0, -10, AnimationDuration),
            indicatorContainer.RotateTo(-180, AnimationDuration),
            accContent.FadeTo(0, 50)
         );
         accContent.IsVisible = false;
      }

      private async void Open()
      {
         accContent.IsVisible = true;
         await Task.WhenAll(
            accContent.TranslateTo(0, 10, AnimationDuration),
            indicatorContainer.RotateTo(0, AnimationDuration),
            accContent.FadeTo(30, 50, Easing.SinIn)
         );
      }

      private void TitleTapped(object sender, EventArgs e)
      {
         IsOpen = !IsOpen;
      }
   }
}