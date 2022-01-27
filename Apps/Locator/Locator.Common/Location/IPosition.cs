namespace Locator.Common.Location
{
   /// <summary>
   ///    The position interface.
   /// </summary>
   public interface IPosition
   {
      /// <summary>
      ///    Gets or sets the latitude.
      /// </summary>
      /// <value>The latitude.</value>
      double Latitude { get; set; }

      /// <summary>
      ///    Gets or sets the longitude.
      /// </summary>
      /// <value>The longitude.</value>
      double Longitude { get; set; }

      /// <summary>
      ///    Gets or sets the address.
      /// </summary>
      /// <value>The address.</value>
      string Address { get; set; }
   }
}