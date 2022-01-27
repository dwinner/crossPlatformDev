namespace AudioPlayerApp.Core.Sound
{
   public interface ISoundHandler
   {
      /// <summary>
      ///    Gets or sets the is playing.
      /// </summary>
      /// <value>The is playing.</value>
      bool IsPlaying { get; set; }

      /// <summary>
      ///    Load this instance.
      /// </summary>
      void Load();

      /// <summary>
      ///    Plaies the pause.
      /// </summary>
      /// <returns>The pause.</returns>
      void PlayPause();

      /// <summary>
      ///    Stop this instance.
      /// </summary>
      void Stop();

      /// <summary>
      ///    Duration this instance.
      /// </summary>
      double Duration();

      /// <summary>
      ///    Sets the position.
      /// </summary>
      /// <returns>The position.</returns>
      /// <param name="value">Value.</param>
      void SetPosition(double value);

      /// <summary>
      ///    Currents the position.
      /// </summary>
      /// <returns>The position.</returns>
      double CurrentPosition();

      /// <summary>
      ///    Forward this instance.
      /// </summary>
      void Forward();

      /// <summary>
      ///    Rewind this instance.
      /// </summary>
      void Rewind();
   }
}