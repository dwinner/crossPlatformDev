namespace StarWarsSample.Forms.UI.Components
{
   public partial class MenuOptionView
   {
      public MenuOptionView()
      {
         InitializeComponent();
      }

      public string Text
      {
         set => label.Text = value;
      }

      public string Source
      {
         set => icon.Source = value;
      }
   }
}