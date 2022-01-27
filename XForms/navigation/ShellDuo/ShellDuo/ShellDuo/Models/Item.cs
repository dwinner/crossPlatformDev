namespace ShellDuo.Models
{
   public class Item : BaseModel
   {
      private string _description = string.Empty;
      private string _id = string.Empty;
      private string _text = string.Empty;

      public string Id
      {
         get => _id;
         set => SetProperty(ref _id, value);
      }

      public string Text
      {
         get => _text;
         set => SetProperty(ref _text, value);
      }

      public string Description
      {
         get => _description;
         set => SetProperty(ref _description, value);
      }
   }
}