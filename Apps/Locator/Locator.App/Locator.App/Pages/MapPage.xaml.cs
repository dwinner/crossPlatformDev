using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Locator.App.UI;
using Locator.Common.Location;
using Locator.Common.ViewModels;
using Xamarin.Forms.Maps;
using Position = Xamarin.Forms.Maps.Position;

namespace Locator.App.Pages
{
   /// <summary>
   ///    Map page.
   /// </summary>
   public partial class MapPage : INavigableXamarinFormsPage
   {
      /// <summary>
      ///    The geocoder.
      /// </summary>
      private readonly Geocoder _geocoder;

      /// <summary>
      ///    The view model.
      /// </summary>
      private readonly MapPageViewModel _viewModel;

      /// <summary>
      ///    The closest subscriptions.
      /// </summary>
      private IDisposable _closestSubscriptions;

      /// <summary>
      ///    The location update subscriptions.
      /// </summary>
      private IDisposable _locationUpdateSubscriptions;

      /// <summary>
      ///    Initializes a new instance of the <see cref="T:Locator.App.Pages.MapPage" /> class.
      /// </summary>
      public MapPage() => InitializeComponent();

      /// <summary>
      ///    Initializes a new instance of the <see cref="T:Locator.App.Pages.MapPage" /> class.
      /// </summary>
      /// <param name="model">Model.</param>
      public MapPage(MapPageViewModel model)
      {
         _viewModel = model;
         BindingContext = model;
         InitializeComponent();
         Appearing += HandleAppearing;
         Disappearing += HandleDisappearing;
         _geocoder = new Geocoder();
      }

      /// <summary>
      ///    Called when page is navigated to.
      /// </summary>
      /// <returns>The navigated to.</returns>
      /// <param name="navigationParameters">Navigation parameters.</param>
      public void OnNavigatedTo(IDictionary<string, object> navigationParameters) => this.Show(navigationParameters);

      /// <summary>
      ///    Handles the disappearing.
      /// </summary>
      /// <returns>The disappearing.</returns>
      /// <param name="sender">Sender.</param>
      /// <param name="e">E.</param>
      private void HandleDisappearing(object sender, EventArgs e)
      {
         _viewModel.OnDisppear();
         _locationUpdateSubscriptions?.Dispose();
         _closestSubscriptions?.Dispose();
      }

      /// <summary>
      ///    Handles the appearing.
      /// </summary>
      /// <returns>The appearing.</returns>
      /// <param name="sender">Sender.</param>
      /// <param name="e">E.</param>
      private void HandleAppearing(object sender, EventArgs e)
      {
         _viewModel.OnAppear();
         _locationUpdateSubscriptions = _viewModel.LocationUpdates.Subscribe(LocationChanged);
         _closestSubscriptions = _viewModel.ClosestUpdates.Subscribe(ClosestChanged);
      }

      /// <summary>
      ///    Locations the changed.
      /// </summary>
      /// <returns>The changed.</returns>
      /// <param name="position">Position.</param>
      private void LocationChanged(IPosition position)
      {
         try
         {
            var formsPosition = new Position(position.Latitude, position.Longitude);

            _geocoder.GetAddressesForPositionAsync(formsPosition)
               .ContinueWith(_ =>
               {
                  var mostRecent = _.Result.FirstOrDefault();
                  if (mostRecent != null)
                  {
                     _viewModel.Address = mostRecent;
                  }
               })
               .ConfigureAwait(false);
            mapView.MoveToRegion(MapSpan.FromCenterAndRadius(formsPosition, Distance.FromMiles(0.3)));
         }
         catch (Exception e)
         {
            Debug.WriteLine("MapPage: Error with moving map region - " + e);
         }
      }

      /// <summary>
      ///    Closests the changed.
      /// </summary>
      /// <returns>The changed.</returns>
      /// <param name="position">Position.</param>
      private void ClosestChanged(IPosition position)
      {
         try
         {
            var pin = new Pin
            {
               Type = PinType.Place,
               Position = new Position(position.Latitude, position.Longitude),
               Label = "Closest Location",
               Address = position.Address
            };

            mapView.Pins.Add(pin);
            mapView.MoveToRegion(
               MapSpan.FromCenterAndRadius(
                  new Position(position.Latitude, position.Longitude),
                  Distance.FromMiles(0.3)));
         }
         catch (Exception e)
         {
            Debug.WriteLine("MapPage: Error with moving pin - " + e);
         }
      }
   }
}