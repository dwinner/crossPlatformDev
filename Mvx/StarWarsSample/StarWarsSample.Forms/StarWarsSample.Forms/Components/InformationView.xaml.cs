namespace StarWarsSample.Forms.UI.Components
{
   public partial class InformationView
   {
      public InformationView()
      {
         InitializeComponent();
      }

      public string LabelText
      {
         set => label.Text = value;
      }

      public string ValueText
      {
         set => this.value.Text = value;
      }
   }
}