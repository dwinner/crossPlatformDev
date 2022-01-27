using System.Collections.Generic;

namespace PaperBoy.Helpers
{
   public interface IFileHelper
   {
      string GetLocalFilePath(string fileName);
      List<string> GetSpecialFolders();
   }
}