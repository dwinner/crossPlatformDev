using System;
using System.Diagnostics;
using System.Net.Http;
using System.Windows.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace ServerInteropApp
{
   public class RateViewModel : ViewModelBase
   {
      private const string YahooUrl =
         "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20csv%20where%20url%3D%22http%3A%2F%2Ffinance.yahoo.com%2Fd%2Fquotes.csv%3Fe%3D.csv%26f%3Dnl1d1t1%26s%3Dusdeur%3DX%22%3B&format=json&callback=";

      private decimal _ask;
      private decimal _bid;
      private ICommand _loadDataCommand;
      private decimal _rate;

      public decimal Rate
      {
         get => _rate;
         set
         {
            _rate = value;
            OnPropertyChanged();
         }
      }

      public decimal Ask
      {
         get => _ask;
         set
         {
            _ask = value;
            OnPropertyChanged();
         }
      }

      public decimal Bid
      {
         get => _bid;
         set
         {
            _bid = value;
            OnPropertyChanged();
         }
      }

      public ICommand LoadDataCommand =>
         _loadDataCommand ?? (_loadDataCommand = new Command(async () =>
         {
            try
            {
               using (var client = new HttpClient {BaseAddress = new Uri(YahooUrl)})
               {
                  var response = await client.GetAsync(client.BaseAddress).ConfigureAwait(true);
                  response.EnsureSuccessStatusCode();

                  var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                  var jObject = JObject.Parse(content);
                  var selectedToken = jObject.SelectToken(@"$.query.results.rate");
                  var rateInfo = JsonConvert.DeserializeObject<RateInfo>(selectedToken.ToString());

                  Rate = rateInfo.Rate;
                  Ask = rateInfo.Ask;
                  Bid = rateInfo.Bid;
               }
            }
            catch (Exception ex)
            {
               Debug.WriteLine(ex.Message);
            }
         }));
   }
}