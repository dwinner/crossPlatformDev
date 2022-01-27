using PaperBoy.Common;

namespace PaperBoy.Models
{
   public class UserInformation : ObservableBase
   {
      private string _bioContent;
      private string _displayName;
      private string _profileImage;

      public string DisplayName
      {
         get => _displayName;
         set => SetProperty(ref _displayName, value);
      }

      public string BioContent
      {
         get => _bioContent;
         set => SetProperty(ref _bioContent, value);
      }

      public string ProfileImage
      {
         get => _profileImage;
         set => SetProperty(ref _profileImage, value);
      }
   }
}