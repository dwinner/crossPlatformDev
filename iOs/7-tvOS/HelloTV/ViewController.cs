using System;
using System.Linq;
using System.Threading.Tasks;
using HelloTV.Enums;
using HelloTV.Helpers;
using HelloTV.Models;
using UIKit;

namespace HelloTV
{
   public partial class ViewController : UIViewController
   {
      private const TemperatureUnit TemperatureUnit = Enums.TemperatureUnit.Metric;

      public ViewController(IntPtr handle) : base(handle)
      {
      }

      public override async void ViewDidLoad()
      {
         base.ViewDidLoad();

         await DisplayWeatherInfoAsync(null).ConfigureAwait(true);
         ButtonGetCurrentWeather.PrimaryActionTriggered += ButtonGetCurrentWeather_PrimaryActionTriggered;
      }

      private async void ButtonGetCurrentWeather_PrimaryActionTriggered(object sender, EventArgs e)
      {
         var cityName = TextFieldCityName.Text;

         if (string.IsNullOrEmpty(cityName))
         {
            DisplayOkAlert("Please enter the city name");
         }
         else
         {
            try
            {
               var weatherInfo = await WeatherServiceHelper.GetWeatherInfoAsync(cityName, TemperatureUnit)
                  .ConfigureAwait(true);
               await DisplayWeatherInfoAsync(weatherInfo).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
               DisplayOkAlert(ex.Message);
            }
         }
      }

      private async Task DisplayWeatherInfoAsync(WeatherInfo weatherInfo)
      {
         const string alternateSymbol = "--";
         var tempUnit = WeatherServiceHelper.GetTemperatureUnitString(TemperatureUnit);
         const string pressureUnit = "hPa";

         if (weatherInfo == null)
         {
            LabelTemperature.Text = $"Temperature: {alternateSymbol}";
            LabelHumidity.Text = $"Humidity: {alternateSymbol}";
            LabelPressure.Text = $"Pressure: {alternateSymbol}";
            LabelDescription.Text = $"Description: {alternateSymbol}";
         }
         else
         {
            LabelTemperature.Text = $"Temperature: {weatherInfo.Main.Temp} {tempUnit}";
            LabelHumidity.Text = $"Humidity: {weatherInfo.Main.Humidity} %";
            LabelPressure.Text = $"Pressure: {weatherInfo.Main.Pressure} {pressureUnit}";
            LabelDescription.Text = $"Description: {weatherInfo.Weather.FirstOrDefault().Description}";
            ImageViewWeatherIcon.Image = await IconHelper.GetIconAsync(weatherInfo).ConfigureAwait(true);
         }
      }

      private void DisplayOkAlert(string message)
      {
         var controller = UIAlertController.Create("HelloTV", message, UIAlertControllerStyle.Alert);
         var okAction = UIAlertAction.Create("OK", UIAlertActionStyle.Default, null);
         controller.AddAction(okAction);
         PresentViewController(controller, true, null);
      }
   }
}