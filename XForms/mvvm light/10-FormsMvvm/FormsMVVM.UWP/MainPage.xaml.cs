﻿namespace FormsMVVM.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new FormsMVVM.App());
      }
   }
}