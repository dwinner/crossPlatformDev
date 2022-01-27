using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EditableLabel
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class EditableLabel
   {
      public static readonly BindableProperty IsFocusedModeProperty = BindableProperty.Create("IsFocusedMode",
         typeof(bool), typeof(EditableLabel), default(bool), propertyChanged: OnIsFocusedModePropertyChanged);

      public static readonly BindableProperty ContentChangedProperty = BindableProperty.Create("ContentChanged",
         typeof(bool), typeof(EditableLabel), default(bool), propertyChanged: OnContentChangedPropertyChanged);

      public static readonly BindableProperty TextProperty =
         BindableProperty.Create("Text", typeof(string), typeof(EditableLabel), default(string));

      public static readonly BindableProperty FontSizeProperty =
         BindableProperty.Create("FontSize", typeof(float), typeof(EditableLabel), 12f);

      public static readonly BindableProperty PlaceHolderProperty =
         BindableProperty.Create("PlaceHolder", typeof(string), typeof(EditableLabel), default(string));

      public static readonly BindableProperty FontFamilyProperty =
         BindableProperty.Create("FontFamily", typeof(string), typeof(EditableLabel), default(string));

      public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create("FontAttributes",
         typeof(FontAttributes), typeof(EditableLabel), default(FontAttributes));

      public static readonly BindableProperty TextColorProperty =
         BindableProperty.Create("TextColor", typeof(Color), typeof(EditableLabel), default(Color));

      public EditableLabel()
      {
         InitializeComponent();
      }

      public bool IsFocusedMode
      {
         get => (bool) GetValue(IsFocusedModeProperty);
         set => SetValue(IsFocusedModeProperty, value);
      }

      public bool ContentChanged
      {
         get => (bool) GetValue(ContentChangedProperty);
         private set => SetValue(ContentChangedProperty, value);
      }

      public string Text
      {
         get => (string) GetValue(TextProperty);
         set => SetValue(TextProperty, value);
      }

      public float FontSize
      {
         get => (float) GetValue(FontSizeProperty);
         set => SetValue(FontSizeProperty, value);
      }

      public string PlaceHolder
      {
         get => (string) GetValue(PlaceHolderProperty);
         set => SetValue(PlaceHolderProperty, value);
      }

      public string FontFamily
      {
         get => (string) GetValue(FontFamilyProperty);
         set => SetValue(FontFamilyProperty, value);
      }

      public FontAttributes FontAttributes
      {
         get => (FontAttributes) GetValue(FontAttributesProperty);
         set => SetValue(FontAttributesProperty, value);
      }

      public Color TextColor
      {
         get => (Color) GetValue(TextColorProperty);
         set => SetValue(TextColorProperty, value);
      }

      private static async void OnIsFocusedModePropertyChanged(BindableObject bindable, object oldValue,
         object newValue)
      {
         if ((bool) newValue)
         {
            await Task.Run(() =>
            {
               Thread.Sleep(100);
               Device.BeginInvokeOnMainThread(() =>
               {
                  (bindable as EditableLabel).MainEntry.Focus();
                  (bindable as EditableLabel).MainEntry.CursorPosition =
                     (bindable as EditableLabel).MainEntry.Text?.Length ?? 0;
               });
            });
         }
      }

      private static async void OnContentChangedPropertyChanged(BindableObject bindable, object oldValue,
         object newValue)
      {
         if ((bool) newValue)
         {
            await Task.Run(() =>
            {
               Thread.Sleep(100);
               Device.BeginInvokeOnMainThread(() =>
               {
                  (bindable as EditableLabel).MainEntry.Focus();
                  (bindable as EditableLabel).MainEntry.CursorPosition =
                     (bindable as EditableLabel).MainEntry.Text?.Length ?? 0;
               });
            });
         }
      }

      private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
      {
         IsFocusedMode = true;
      }
   }
}