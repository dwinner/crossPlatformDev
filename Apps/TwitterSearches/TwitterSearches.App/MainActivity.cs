using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using AppLayouts = AppDevUnited.TwitterSearches.App.Resource.Layout;
using AppIds = AppDevUnited.TwitterSearches.App.Resource.Id;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace AppDevUnited.TwitterSearches.App
{
   /// <summary>
   ///    Управление поисковыми запросами "Твиттера" и вывод результатов в браузере
   /// </summary>
   [Activity(
      Label = "@string/app_name",
      Theme = "@style/AppTheme.NoActionBar",
      MainLauncher = true,
      WindowSoftInputMode = SoftInput.StateAlwaysHidden)]
   public partial class MainActivity : AppCompatActivity
   {
      // Имя файла SharedPreferences с сохраненными запросами
      private const string Searches = nameof(Searches);
      private SearchesAdapter _adapter; // Для связывания данных с RecyclerView
      private View.IOnClickListener _itemClickListener;
      private View.IOnLongClickListener _itemLongClickListener;
      private EditText _queryEditText; // Для ввода запроса
      private ISharedPreferences _savedSearches; // Сохраненные запросы
      private FloatingActionButton _saveFloatingActionButton; // Для сохранения
      private EditText _tagEditText; // Для ввода тэга
      private List<string> _tags; // Список тэгов сохраненных запросов

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(AppLayouts.activity_main);

         var toolbar = FindViewById<Toolbar>(AppIds.toolbar);
         SetSupportActionBar(toolbar);

         // Получить ссылки на EditText и добавить слушателей
         _queryEditText = FindViewById<TextInputLayout>(AppIds.queryTextInputLayout).EditText;
         _queryEditText.TextChanged += OnTextChanged;
         _tagEditText = FindViewById<TextInputLayout>(AppIds.tagTextInputLayout).EditText;
         _tagEditText.TextChanged += OnTextChanged;

         // Получить объект SharedPreferences с сохраненными запросами
         _savedSearches = GetSharedPreferences(Searches, FileCreationMode.Private);

         // Получить сохраненные тэги и отсортировать их
         _tags = new List<string>(_savedSearches.All.Keys);
         _tags.Sort(StringComparer.CurrentCultureIgnoreCase);

         // Получить ссылку на RecyclerView и настроить его
         var recyclerView = FindViewById<RecyclerView>(AppIds.recyclerView);

         // Получить LinearLayoutManager для вертикального списка
         recyclerView.SetLayoutManager(new LinearLayoutManager(this));

         // Создать RecyclerView.Adapter для связывания тэгов с RecyclerView
         _itemClickListener = new SearchTagItemClickListener(this);
         _itemLongClickListener = new EditSavedQueryItemLongClickListener(this);
         _adapter = new SearchesAdapter(_tags, _itemClickListener, _itemLongClickListener);
         recyclerView.SetAdapter(_adapter);

         // Назначить ItemDecorator для рисования линий между элементами
         recyclerView.AddItemDecoration(new ItemDivider(this));

         // Зарегистрировать обработчик для сохранения и редактирования
         _saveFloatingActionButton = FindViewById<FloatingActionButton>(AppIds.fab);
         _saveFloatingActionButton.SetOnClickListener(new SaveButtonClickListener(this));
         UpdateSaveFab(); // Скрыть кнопку, потому что поля EditText пусты
      }

      private void UpdateSaveFab()
      {
         if (string.IsNullOrEmpty(_queryEditText.Text) || string.IsNullOrEmpty(_tagEditText.Text))
            _saveFloatingActionButton.Hide();
         else
            _saveFloatingActionButton.Show();
      }
   }
}