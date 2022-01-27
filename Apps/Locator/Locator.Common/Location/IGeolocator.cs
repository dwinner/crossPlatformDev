using System.Reactive.Subjects;

namespace Locator.Common.Location
{
   /// <summary>
   ///    Geolocator.
   /// </summary>
   public interface IGeolocator
   {
      /// <summary>
      ///    Gets or sets the positions.
      /// </summary>
      /// <value>The positions.</value>
      Subject<IPosition> Positions { get; set; }

      /// <summary>
      ///    Start this instance.
      /// </summary>
      void Start();

      /// <summary>
      ///    Stop this instance.
      /// </summary>
      void Stop();
   }
}