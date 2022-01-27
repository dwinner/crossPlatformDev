namespace MvvxSandboxApp.Core.Models
{
   public class UsaStateDetail
   {
      public int UsaStateDetailId { get; set; }

      public float Area { get; set; }

      public string Notes { get; set; }

      public string StateUri { get; set; }

      public string FlagImageUri { get; set; }

      public int Municipal { get; set; }

      public int Metropolian { get; set; }

      public int StateRank { get; set; }

      public int CountryRank { get; set; }

      public UsaState UsaState { get; set; }
   }
}