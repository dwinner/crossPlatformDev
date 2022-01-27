using Xamarin.Forms;

namespace ListViewSample
{
   public class CustomCell : ViewCell
   {
      public static readonly BindableProperty TitleProperty =
         BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomCell), string.Empty);

      public static readonly BindableProperty ImagePathProperty =
         BindableProperty.Create(nameof(ImagePath), typeof(ImageSource), typeof(CustomCell));

      public static readonly BindableProperty ImageWidthProperty =
         BindableProperty.Create(nameof(ImageWidth), typeof(int), typeof(CustomCell), 100);

      public static readonly BindableProperty ImageHeightProperty =
         BindableProperty.Create(nameof(ImageHeight), typeof(int), typeof(CustomCell), 100);

      public static readonly BindableProperty DetailProperty =
         BindableProperty.Create(nameof(Detail), typeof(string), typeof(CustomCell), string.Empty);

      private readonly Label _detailLabel;
      private readonly Image _image;
      private readonly Label _titleLabel;

      public CustomCell()
      {
         _titleLabel = new Label {FontSize = 18};
         _detailLabel = new Label();
         _image = new Image();

         var cell = new StackLayout {Orientation = StackOrientation.Horizontal};
         var titleDetaiLayout = new StackLayout();
         titleDetaiLayout.Children.Add(_titleLabel);
         titleDetaiLayout.Children.Add(_detailLabel);

         cell.Children.Add(_image);
         cell.Children.Add(titleDetaiLayout);

         View = cell;
      }

      public string Title
      {
         get => (string) GetValue(TitleProperty);
         set => SetValue(TitleProperty, value);
      }

      public int ImageWidth
      {
         get => (int) GetValue(ImageWidthProperty);
         set => SetValue(ImageWidthProperty, value);
      }

      public int ImageHeight
      {
         get => (int) GetValue(ImageHeightProperty);
         set => SetValue(ImageHeightProperty, value);
      }

      public ImageSource ImagePath
      {
         get => (ImageSource) GetValue(ImagePathProperty);
         set => SetValue(ImagePathProperty, value);
      }

      public string Detail
      {
         get => (string) GetValue(DetailProperty);
         set => SetValue(DetailProperty, value);
      }

      protected override void OnBindingContextChanged()
      {
         base.OnBindingContextChanged();

         if (BindingContext != null)
         {
            _titleLabel.Text = Title;
            _detailLabel.Text = Detail;
            _image.Source = ImagePath;
            _image.WidthRequest = ImageWidth;
            _image.HeightRequest = ImageHeight;
         }
      }
   }
}