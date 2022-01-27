namespace Chat.Common.Presenter
{
   /// <summary>
   ///    View.
   /// </summary>
   public interface IView
   {
      /// <summary>
      ///    The is in.
      /// </summary>
      bool IsInProgress { get; set; }

      /// <summary>
      ///    Sets the error message.
      /// </summary>
      /// <returns>The error message.</returns>
      /// <param name="message">Message.</param>
      void SetErrorMessage(string message);
   }
}