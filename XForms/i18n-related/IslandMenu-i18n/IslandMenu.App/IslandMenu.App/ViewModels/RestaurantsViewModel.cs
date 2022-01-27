using System;
using System.Collections.Generic;
using IslandMenu.App.Interfaces;
using IslandMenu.App.Models;
using IslandMenu.App.Services;
using Xamarin.Forms;

namespace IslandMenu.App.ViewModels
{
   public class RestaurantsViewModel
   {
      public RestaurantsViewModel()
      {
         ImageUrl = "islandmenubanner.jpg";
         Restaurants = GetRestaurants();
      }

      public string ImageUrl { get; }

      public string LastUpdate { get; private set; }

      public IEnumerable<Restaurant> Restaurants { get; }

      public INavigation Navigation { get; set; }

      private IEnumerable<Restaurant> GetRestaurants()
      {
         var dataServices = new DataServices();
         LastUpdate = string.Format(Resources.IslandMenu.LastUpdate, DateTime.Now.ToString("D"));
         var cultureInfo = DependencyService.Get<ICultureInfo>();
         // ReSharper disable AsyncConverter.AsyncWait
         return dataServices.GetRestaurantsAsync(cultureInfo.CurrentCulture.Name).Result;
         // ReSharper restore AsyncConverter.AsyncWait
      }
   }
}