namespace FileStorageApp.Logging
{
   /// <summary>
   ///    The Debug interface
   /// </summary>
   public interface ILogger
   {
      /// <summary>
      ///    Write line
      /// </summary>
      /// <param name="message">Message</param>
      void WriteLine(string message);

      /// <summary>
      ///    Write line with var-args
      /// </summary>
      /// <param name="message">Message</param>
      /// <param name="args">Arguments</param>
      void WriteLineTime(string message, params object[] args);
   }
}