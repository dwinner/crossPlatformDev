using Microsoft.Cognitive.LUIS;

namespace SmartApp
{
   internal class IntentHandler
   {
      [IntentHandler(0.5, Name = "OnTheLight")]
      public void IdentifyObstacle(LuisResult result, object context)
      {
         var activity = context as MainActivity;
         activity.WriteInterpretation("The light was turned on.");
      }

      [IntentHandler(0.5, Name = "None")]
      public void None(LuisResult result, object context)
      {
         var activity = context as MainActivity;
         activity.WriteInterpretation("I couldn't understand you.");
      }

      [IntentHandler(0.5, Name = "OffTheLight")]
      public void SetPictures(LuisResult result, object context)
      {
         var activity = context as MainActivity;
         activity.WriteInterpretation("The light was turned off.");
      }
   }
}