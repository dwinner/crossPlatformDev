﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Application.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using UIKit;

namespace Camera.iOS
{
   public class Application
   {
      // This is the main entry point of the application.
      private static void Main(string[] args)
      {
         // if you want to use a different Application Delegate class from "AppDelegate"
         // you can specify it here.
         UIApplication.Main(args, null, "AppDelegate");
      }
   }
}