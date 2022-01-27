using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Net;
using JavaObj = Java.Lang.Object;
using ResId = WeatherViewer.App.Resource.Id;
using ResLayout = WeatherViewer.App.Resource.Layout;
using ResString = WeatherViewer.App.Resource.String;

namespace WeatherViewer.App
{
   /// <summary>
   ///    Объект ArrayAdapter для отображения элементов List'Weather' в ListView
   /// </summary>
   public class WeatherArrayAdapter : ArrayAdapter<Weather>
   {
      // Кэш для уже загруженных объектов Bitmap
      private readonly Dictionary<string, Bitmap> _bitmaps = new Dictionary<string, Bitmap>();

      public WeatherArrayAdapter(Context context, IList<Weather> forecast)
         : base(context, -1, forecast)
      {
      }

      public override View GetView(int position, View convertView, ViewGroup parent)
      {
         // Получение объекта Weather для заданной позиции ListView
         var day = GetItem(position);
         ViewHolder viewHolder; // Объект, содержащий ссылки на представления элемента списка

         // Проверить возможность повторного использования ViewHolder для элемента, вышедшего за границы экрана
         if (convertView == null) // Объекта ViewHolder нет, создать его
         {
            var inflater = LayoutInflater.From(Context);
            convertView = inflater.Inflate(ResLayout.ListItem, parent, false);
            viewHolder = new ViewHolder
            {
               ConditionImageView = convertView.FindViewById<ImageView>(ResId.conditionImageView),
               DayTextView = convertView.FindViewById<TextView>(ResId.dayTextView),
               LowTextView = convertView.FindViewById<TextView>(ResId.lowTextView),
               HiTextView = convertView.FindViewById<TextView>(ResId.hiTextView),
               HumidityTextView = convertView.FindViewById<TextView>(ResId.humidityTextView)
            };

            convertView.Tag = viewHolder;
         }
         else
         {
            // Существующий объект используется заново
            viewHolder = (ViewHolder) convertView.Tag;
         }

         // Если значок погодных условий уже загружен, использовать его;
         // в противном случае загрузить в отдельном потоке
         if (_bitmaps.ContainsKey(day.IconUrl))
         {
            viewHolder.ConditionImageView.SetImageBitmap(_bitmaps[day.IconUrl]);
         }
         else
         {
            // Загрузить и вывести значок погодных условий
            var loadImageTask = new LoadImageTask(viewHolder.ConditionImageView, this);
            loadImageTask.Execute(day.IconUrl);
         }

         // Получить данные из объекта Weather и заполнить представления
         viewHolder.DayTextView.Text = Context.GetString(ResString.day_description, day.DayOfWeek, day.Description);
         viewHolder.LowTextView.Text = Context.GetString(ResString.low_temp, day.MinTemp);
         viewHolder.HiTextView.Text = Context.GetString(ResString.high_temp, day.MaxTemp);
         viewHolder.HumidityTextView.Text = Context.GetString(ResString.humidity, day.Humidity);

         return convertView; // Вернуть готовое представление элемента
      }

      /// <summary>
      ///    Класс для повторного использования представлений списка при прокрутке
      /// </summary>
      private sealed class ViewHolder : JavaObj
      {
         internal ImageView ConditionImageView { get; set; }

         internal TextView DayTextView { get; set; }

         internal TextView LowTextView { get; set; }

         internal TextView HiTextView { get; set; }

         internal TextView HumidityTextView { get; set; }
      }

      /// <summary>
      ///    Асинхронная загрузка изображения значка погодных условий
      /// </summary>
      private sealed class LoadImageTask : AsyncTask<string, Void, Bitmap>
      {
         private const string Tag = nameof(LoadImageTask);
         private readonly WeatherArrayAdapter _adapter;
         private readonly ImageView _imageView; // Для вывода миниатюры

         /// <summary>
         ///    Сохранение ImageView для загруженного объекта Bitmap
         /// </summary>
         /// <param name="imageView">Image view</param>
         /// <param name="adapter">Outer link</param>
         public LoadImageTask(ImageView imageView, WeatherArrayAdapter adapter)
         {
            _imageView = imageView;
            _adapter = adapter;
         }

         protected override Bitmap RunInBackground(params string[] @params)
         {
            Bitmap bitmap = null;
            var iconUrl = @params[0];

            try
            {
               var url = new URL(iconUrl); // Создать URL для изображения

               // Открыть объект HttpURLConnection, получить InputStream и загрузить изображение
               using (var connection = (HttpURLConnection) url.OpenConnection())
               using (var inputStream = connection.InputStream)
               {
                  bitmap = BitmapFactory.DecodeStream(inputStream);
                  _adapter._bitmaps.Add(iconUrl, bitmap);
               }
            }
            catch (Exception e)
            {
               Log.Error(Tag, e.Message);
            }

            return bitmap;
         }

         protected override void OnPostExecute(Bitmap result) => _imageView.SetImageBitmap(result);
      }
   }
}