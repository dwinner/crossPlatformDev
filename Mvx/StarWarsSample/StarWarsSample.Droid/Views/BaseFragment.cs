﻿using Android.Content.Res;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.Widget;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.Platforms.Android.Views.AppCompat;
using MvvmCross.Platforms.Android.Views.Fragments;
using MvvmCross.ViewModels;

namespace StarWarsSample.Droid.Views
{
   public abstract class BaseFragment : MvxFragment
   {
      private MvxActionBarDrawerToggle _drawerToggle;
      private Toolbar _toolbar;

      public MvxActivity ParentActivity => (MvxActivity) Activity;

      protected abstract int FragmentId { get; }

      public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
      {
         base.OnCreateView(inflater, container, savedInstanceState);

         var view = this.BindingInflate(FragmentId, null);
         _toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);
         if (_toolbar != null)
         {
            ParentActivity.SetSupportActionBar(_toolbar);
            ParentActivity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _drawerToggle = new MvxActionBarDrawerToggle(
               Activity, // host Activity
               ((MainView) ParentActivity).DrawerLayout, // DrawerLayout object
               _toolbar, // nav drawer icon to replace 'Up' caret
               Resource.String.drawer_open, // "open drawer" description
               Resource.String.drawer_close // "close drawer" description
            );
            _drawerToggle.DrawerOpened += (sender, e) => ((MainView) Activity)?.HideSoftKeyboard();
            ((MainView) ParentActivity).DrawerLayout.AddDrawerListener(_drawerToggle);
         }

         return view;
      }

      public override void OnConfigurationChanged(Configuration newConfig)
      {
         base.OnConfigurationChanged(newConfig);
         if (_toolbar != null)
         {
            _drawerToggle.OnConfigurationChanged(newConfig);
         }
      }

      public override void OnActivityCreated(Bundle savedInstanceState)
      {
         base.OnActivityCreated(savedInstanceState);
         if (_toolbar != null)
         {
            _drawerToggle.SyncState();
         }
      }
   }

   public abstract class BaseFragment<TViewModel> : BaseFragment where TViewModel : class, IMvxViewModel
   {
      public new TViewModel ViewModel
      {
         get => (TViewModel) base.ViewModel;
         set => base.ViewModel = value;
      }
   }
}