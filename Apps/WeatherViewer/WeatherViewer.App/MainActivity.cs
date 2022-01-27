using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Java.IO;
using Java.Net;
using Org.Json;
using JavaException = Java.Lang.Exception;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Ids = WeatherViewer.App.Resource.Id;
using Strings = WeatherViewer.App.Resource.String;
using JVoid = Java.Lang.Void;

namespace WeatherViewer.App
{
   [Activity(Label = "@string/app_name",
      Theme = "@style/AppTheme.NoActionBar",
      MainLauncher = true,
      ScreenOrientation = ScreenOrientation.Portrait)]
   public class MainActivity : AppCompatActivity
   {
      private const string ErrorTag = nameof(MainActivity);

      private readonly List<Weather>
         _weatherList = new List<Weather>(); // Список объектов Weather, представляющих прогнох погоды

      private WeatherArrayAdapter _weatherArrayAdapter; // Связывает объекты Weather с элементами ListView
      private ListView _weatherListView; // Для вывода информации

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(Resource.Layout.ActivityMain);

         var toolbar = FindViewById<SupportToolbar>(Ids.toolbar);
         SetSupportActionBar(toolbar);

         // ArrayAdapter для связывания weatherList с weatherListView
         _weatherListView = FindViewById<ListView>(Ids.weatherListView);
         _weatherListView.Adapter = _weatherArrayAdapter = new WeatherArrayAdapter(this, _weatherList);

         // FAB скрывает клавиатуру и выдает запрос к веб-сервису
         var fab = FindViewById<FloatingActionButton>(Ids.fab);
         fab.Click += OnFabClick;
      }

      private void OnFabClick(object sender, EventArgs eventArgs)
      {
         // Получить текст из locationEditText и создать URL веб-сервисы
         var locationEditText = FindViewById<EditText>(Ids.locationEditText);
         var url = CreateUrl(locationEditText.Text);

         // Скрыть клавиатуру и запустить GetWeatherTask для получения погодных данных от OpenWeatherMap.org
         if (url != null)
         {
            DismissKeyboard(locationEditText);
            var getLocalWeatherTask = new GetWeatherTask(this);
            getLocalWeatherTask.Execute(url);
         }
         else
         {
            Snackbar.Make(
                  FindViewById<CoordinatorLayout>(Ids.coordinatorLayout), Strings.invalid_url, Snackbar.LengthLong)
               .Show();
         }
      }

      /// <summary>
      ///    Закрыть клавиатуру при нажатии на FloatingActionButton
      /// </summary>
      /// <param name="view">Представление, инициирующее сокрытие</param>
      private void DismissKeyboard(View view)
      {
         var inputMethodManager = (InputMethodManager) GetSystemService(InputMethodService);
         inputMethodManager.HideSoftInputFromWindow(view.WindowToken, 0);
      }

      /// <summary>
      ///    Создание URL веб-сервисы openweathermap.org для названия города
      /// </summary>
      /// <param name="city">Название города</param>
      /// <returns>Нужный EndPoint URL</returns>
      private URL CreateUrl(string city)
      {
         var apiKey = GetString(Strings.api_key);
         var baseUrl = GetString(Strings.web_service_url);

         try
         {
            // Создание URL для заданного города и температурной шкалы
            var urlString = $"{baseUrl}{URLEncoder.Encode(city, "UTF-8")}&units=imperial&cnt=16&APPID={apiKey}";
            return new URL(urlString);
         }
         catch (JavaException javaEx)
         {
            Log.Error(ErrorTag, javaEx, javaEx.Message);
            return null;
         }
      }

      /// <summary>
      ///    Создание объектов Weather на базе JSONObject с прогнозом
      /// </summary>
      /// <param name="forecast">Информация о погоде</param>
      private void ConvertJsonToList(JSONObject forecast)
      {
         _weatherList.Clear();

         try
         {
            var list = forecast.GetJSONArray("list"); // Получение свойства "list" JSONArray

            // Преобразовать каждый элемент списка в объект Weather
            for (var i = 0; i < list.Length(); i++)
            {
               var day = list.GetJSONObject(i); // Данные за день
               var temperatures = day.GetJSONObject("temp"); // Температуры дня
               var weather = day.GetJSONArray("weather").GetJSONObject(0); // Описание и значок

               // Добавить новый объект в список
               _weatherList.Add(new Weather(
                  day.GetLong("dt"), // Временная метка
                  temperatures.GetDouble("min"), // Минимальная температура
                  temperatures.GetDouble("max"), // Максимальная температура
                  day.GetDouble("humidity"), // Процент влажности
                  weather.GetString("description"), // Погодные условия
                  weather.GetString("icon"))); // Имя значка
            }
         }
         catch (JSONException jsonEx)
         {
            Log.Error(ErrorTag, jsonEx, jsonEx.Message);
         }
      }

      /// <summary>
      ///    Обращение к REST-совместимому веб-сервису за погодными данными
      /// </summary>
      private sealed class GetWeatherTask : AsyncTask<URL, JVoid, JSONObject>
      {
         private readonly MainActivity _context;

         public GetWeatherTask(MainActivity context) => _context = context;

         protected override JSONObject RunInBackground(params URL[] @params)
         {
            var url = @params[0];
            var snackTarget = _context.FindViewById<CoordinatorLayout>(Ids.coordinatorLayout);

            try
            {
               using (var connection = (HttpURLConnection) url.OpenConnection())
               {
                  var response = connection.ResponseCode;

                  if (response == HttpStatus.Ok)
                  {
                     var builder = new StringBuilder();

                     try
                     {
                        using (var reader = new BufferedReader(new InputStreamReader(connection.InputStream)))
                        {
                           string line;
                           while ((line = reader.ReadLine()) != null)
                              builder.Append(line);
                        }
                     }
                     catch (IOException ioEx)
                     {
                        Snackbar.Make(snackTarget, Strings.read_error, Snackbar.LengthLong).Show();
                        Log.Error(ErrorTag, ioEx, ioEx.Message);
                     }

                     return new JSONObject(builder.ToString());
                  }

                  Snackbar.Make(snackTarget, Strings.connect_error, Snackbar.LengthLong).Show();
               }
            }
            catch (JavaException javaEx)
            {
               Snackbar.Make(snackTarget, Strings.connect_error, Snackbar.LengthLong).Show();
               Log.Error(ErrorTag, javaEx, javaEx.Message);
            }

            return null;
         }

         protected override void OnPostExecute(JSONObject result)
         {
            _context.ConvertJsonToList(result); // Заполнение weatherList
            _context._weatherArrayAdapter.NotifyDataSetChanged(); // Обновить модель
            _context._weatherListView.SmoothScrollToPosition(0); // Прокрутить вверх
         }
      }
   }
}