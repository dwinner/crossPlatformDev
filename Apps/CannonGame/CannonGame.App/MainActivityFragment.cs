using Android.Media;
using Android.OS;
using Android.Views;
using FragmentV4 = Android.Support.V4.App.Fragment;
using ResLayout = AppDevUnited.CannonGame.App.Resource.Layout;
using ResId = AppDevUnited.CannonGame.App.Resource.Id;

namespace AppDevUnited.CannonGame.App
{
   /// <summary>
   ///    Создает и управляет <see cref="CannonView" />
   /// </summary>
   public class MainActivityFragment : FragmentV4
   {
      private CannonView _cannonView; // UI для игры

      public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
      {
         base.OnCreateView(inflater, container, savedInstanceState);
         var view = inflater.Inflate(ResLayout.FragmentMain, container, false); // Заполнение макета
         _cannonView = view.FindViewById<CannonView>(ResId.cannonView); // Получение ссылки на CannonView

         return view;
      }

      public override void OnActivityCreated(Bundle savedInstanceState)
      {
         base.OnActivityCreated(savedInstanceState);
         Activity.VolumeControlStream = Stream.Music; // Разрешить использование кнопок управления громкостью
      }

      public override void OnPause()
      {
         base.OnPause();
         _cannonView.StopGame(); // Завершение игры
      }

      public override void OnDestroy()
      {
         base.OnDestroy();
         _cannonView.ReleaseResources(); // Освободить ресурсы
      }
   }
}