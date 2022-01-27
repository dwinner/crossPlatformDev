using System;
using Android.Content;
using Android.Support.V7.App;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using JavaObj = Java.Lang.Object;
using AppStrings = AppDevUnited.TwitterSearches.App.Resource.String;
using AppArrays = AppDevUnited.TwitterSearches.App.Resource.Array;
using Uri = Android.Net.Uri;

namespace AppDevUnited.TwitterSearches.App
{
   public partial class MainActivity
   {
      private void OnTextChanged(object sender, TextChangedEventArgs e) => UpdateSaveFab();

      /// <summary>
      ///    Сохраняет пару "тэг-запрос" в SharedPreferences
      /// </summary>
      private sealed class SaveButtonClickListener : JavaObj, View.IOnClickListener
      {
         private readonly MainActivity _activity;

         public SaveButtonClickListener(MainActivity activity) => _activity = activity;

         public void OnClick(View view)
         {
            var query = _activity._queryEditText.Text;
            var tag = _activity._tagEditText.Text;

            if (!string.IsNullOrEmpty(query) && !string.IsNullOrEmpty(tag))
            {
               // Скрыть экранную клавиатуру
               if (_activity.GetSystemService(InputMethodService) is InputMethodManager inputMethodManager)
                  inputMethodManager.HideSoftInputFromWindow(view.WindowToken, HideSoftInputFlags.None);

               AddTaggedSearch(tag, query); // Добавить/обновить запрос
               _activity._queryEditText.Text = _activity._tagEditText.Text = string.Empty;
               _activity._queryEditText.RequestFocus();
            }
         }

         /// <summary>
         ///    Добавление нового запроса с обновлением всех кнопок
         /// </summary>
         /// <param name="tag">Тэг</param>
         /// <param name="query">Запрос</param>
         private void AddTaggedSearch(string tag, string query)
         {
            // Получение SharedPreferences.Editor для сохранения новой пары
            var preferencesEditor = _activity._savedSearches.Edit();
            preferencesEditor.PutString(tag, query); // Сохранение текущего запроса
            preferencesEditor.Apply(); // Сохранение обновленных настроек

            // Если тэг новый, добавить его, отсортировать и вывести список
            if (!_activity._tags.Contains(tag))
            {
               _activity._tags.Add(tag); // Добавить новый тэг
               _activity._tags.Sort(StringComparer.CurrentCultureIgnoreCase);
               _activity._adapter.NotifyDataSetChanged(); // Обновление тэгов в RecyclerView
            }
         }
      }

      /// <summary>
      ///    Запускает браузер для вывода результатов поиска
      /// </summary>
      private sealed class SearchTagItemClickListener : JavaObj, View.IOnClickListener
      {
         private readonly MainActivity _activity;

         public SearchTagItemClickListener(MainActivity activity) => _activity = activity;

         public void OnClick(View view)
         {
            // Получение строки запроса и создание URL для этого запроса
            if (!(view is TextView textView))
               return;

            var tag = textView.Text;
            var urlString =
               $"{_activity.GetString(AppStrings.search_URL)}{Uri.Encode(_activity._savedSearches.GetString(tag, string.Empty), "UTF-8")}";

            // Создание интента для запуска браузера
            var webIntent = new Intent(Intent.ActionView, Uri.Parse(urlString));
            _activity.StartActivity(webIntent); // Вывести результаты в браузер
         }
      }

      /// <summary>
      ///    Отображает диалоговое окно для пересылки, изменения или удаления сохраненного запроса
      /// </summary>
      private sealed class EditSavedQueryItemLongClickListener : JavaObj, View.IOnLongClickListener
      {
         private readonly MainActivity _activity;

         public EditSavedQueryItemLongClickListener(MainActivity activity) => _activity = activity;

         public bool OnLongClick(View view)
         {
            // Получение тэга, на котором было сделано длинное нажатие
            if (!(view is TextView textView))
               return true;

            var tag = textView.Text;

            // Создание нового объекта AlertDialog
            new AlertDialog.Builder(_activity)
               .SetTitle(_activity.GetString(AppStrings.share_edit_delete_title, tag))
               .SetItems(AppArrays.dialog_items, (sender, args) =>
               {
                  switch (args.Which)
                  {
                     case 0: // share
                        ShareSearch(tag);
                        break;

                     case 1: // edit
                        _activity._tagEditText.Text = tag;
                        _activity._queryEditText.Text = _activity._savedSearches.GetString(tag, string.Empty);
                        break;

                     case 2: // delete
                        DeleteSearch(tag);
                        break;
                  }
               })
               .SetNegativeButton(_activity.GetString(AppStrings.cancel), (sender, args) => { })
               .Create()
               .Show();

            return true;
         }

         /// <summary>
         ///    Удаление запроса после подтверждения операции пользователем
         /// </summary>
         /// <param name="tag">Тэг</param>
         private void DeleteSearch(string tag) =>
            new AlertDialog.Builder(_activity) // Создание нового объекта AlertDialog и назначение сообщения
               .SetMessage(_activity.GetString(AppStrings.confirm_message, tag))
               .SetNegativeButton(_activity.GetString(AppStrings.cancel), (sender, args) => { })
               .SetPositiveButton(_activity.GetString(AppStrings.delete), (sender, args) =>
               {
                  _activity._tags.Remove(tag); // Удаление тэга

                  // Получение SharedPreferences.Editor для удаления запроса
                  var preferencesEditor = _activity._savedSearches.Edit();
                  preferencesEditor.Remove(tag);
                  preferencesEditor.Apply();

                  // Повторное связывание для вывода обновленного списка
                  _activity._adapter.NotifyDataSetChanged();
               })
               .Create()
               .Show();

         /// <summary>
         ///    Выбор приложения для пересылки URL-адреса сохраненного запроса
         /// </summary>
         /// <param name="tag">Тэг</param>
         private void ShareSearch(string tag)
         {
            // Создание URL-адреса, представляющего поисковый запрос
            var urlString =
               $"{_activity.GetString(AppStrings.search_URL)}{Uri.Encode(_activity._savedSearches.GetString(tag, string.Empty), "UTF-8")}";

            // Создание объекта Intent для пересылки
            var shareIntent = new Intent();
            shareIntent.SetAction(Intent.ActionSend);
            shareIntent.PutExtra(Intent.ExtraSubject,
               _activity.GetString(AppStrings.share_subject));
            shareIntent.PutExtra(Intent.ExtraText,
               _activity.GetString(AppStrings.share_message, urlString));
            shareIntent.SetType("text/plain");

            // Вывод списка приложений с возможностью пересылки текста
            _activity.StartActivity(
               Intent.CreateChooser(shareIntent, _activity.GetString(AppStrings.share_search)));
         }
      }
   }
}