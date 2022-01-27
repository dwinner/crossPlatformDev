using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using AppIds = AppDevUnited.TwitterSearches.App.Resource.Id;
using AppLayouts = AppDevUnited.TwitterSearches.App.Resource.Layout;

namespace AppDevUnited.TwitterSearches.App
{
   /// <summary>
   ///    Адаптер для связывания данных с элементами RecyclerView
   /// </summary>
   public sealed class SearchesAdapter : RecyclerView.Adapter
   {
      // Слушатели главной активности, регистрируемые для каждого элемента списка
      private readonly View.IOnClickListener _itemClickListener;
      private readonly View.IOnLongClickListener _itemLongClickListener;

      // Список тэгов для хранения данных элементов RecyclerView
      private readonly List<string> _tags;

      public SearchesAdapter(List<string> tags, View.IOnClickListener itemClickListener,
         View.IOnLongClickListener itemLongClickListener)
      {
         _tags = tags;
         _itemClickListener = itemClickListener;
         _itemLongClickListener = itemLongClickListener;
      }

      /// <summary>
      ///    Возвращение кол-ва элементов, связываемых через адаптер
      /// </summary>
      public override int ItemCount => _tags.Count;

      /// <summary>
      ///    Назначение теста элемента списка для вывода тэга запроса
      /// </summary>
      /// <param name="holder"></param>
      /// <param name="position"></param>
      public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
      {
         if (holder is SearchViewHolder searchViewHolder)
            searchViewHolder.TextView.Text = _tags[position];
      }

      /// <summary>
      ///    Создает новый элемент списка и его объект ViewHolder
      /// </summary>
      /// <param name="parent"></param>
      /// <param name="viewType"></param>
      /// <returns></returns>
      public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
      {
         // Заполнение макета list_item
         var view = LayoutInflater.From(parent.Context).Inflate(AppLayouts.list_item, parent, false);

         // Создание ViewHolder для текущего элемента
         return new SearchViewHolder(view, _itemClickListener, _itemLongClickListener);
      }

      /// <summary>
      ///    Вложенный класс, используемый для реализации пэттерна View-Holder в контексте
      ///    RecyclerView для логики повторного использования представлений
      /// </summary>
      private sealed class SearchViewHolder : RecyclerView.ViewHolder
      {
         public SearchViewHolder(View itemView,
            View.IOnClickListener clickListener,
            View.IOnLongClickListener longClickListener)
            : base(itemView)
         {
            TextView = itemView.FindViewById<TextView>(AppIds.textView);

            // Связывание слушателей с itemView
            itemView.SetOnClickListener(clickListener);
            itemView.SetOnLongClickListener(longClickListener);
         }

         public TextView TextView { get; }
      }
   }
}