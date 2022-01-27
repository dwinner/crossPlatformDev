namespace MvvxSandboxApp.Core.ViewModelResults
{
   public readonly struct ApplyFilterResult
   {
      public ApplyFilterResult((int start, int end) hoodYear, (int start, int end) capitalSinceYear)
      {
         HoodYear = hoodYear;
         CapitalSinceYear = capitalSinceYear;
      }

      public (int start, int end) HoodYear { get; }

      public (int start, int end) CapitalSinceYear { get; }

      public static ApplyFilterResult Empty { get; }
         = new((default, default), (default, default));
   }
}