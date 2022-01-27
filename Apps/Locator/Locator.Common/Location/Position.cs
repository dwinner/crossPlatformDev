namespace Locator.Common.Location
{
   /// <summary>
   ///    The position interface.
   /// </summary>
   public class Position : IPosition
   {
      #region Public Properties

      /// <summary>
      ///    Gets or sets the latitude.
      /// </summary>
      /// <value>The latitude.</value>
      public double Latitude { get; set; }

      /// <summary>
      ///    Gets or sets the longitude.
      /// </summary>
      /// <value>The longitude.</value>
      public double Longitude { get; set; }

      /// <summary>
      ///    Gets or sets the address.
      /// </summary>
      /// <value>The address.</value>
      public string Address { get; set; }

      #endregion
   }
}