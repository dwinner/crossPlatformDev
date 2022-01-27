namespace MvvxSandboxApp.Core.Models
{
   public class UsaState
   {
      public int UsaStateId { get; set; }

      public string StateName { get; set; }

      public string Abbr { get; set; }

      public int HoodYear { get; set; }

      public string Capital { get; set; }

      public int CapitalSinceYear { get; set; }

      public UsaStateDetail UsaStateDetail { get; set; }
   }
}