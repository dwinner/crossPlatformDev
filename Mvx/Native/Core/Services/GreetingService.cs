namespace MvvmCrossDemo.Core.Services
{
   public sealed class GreetingService : IGreetingService
   {
      public string GetGreetingText(string aName) => $"Hello, {aName}";
   }
}