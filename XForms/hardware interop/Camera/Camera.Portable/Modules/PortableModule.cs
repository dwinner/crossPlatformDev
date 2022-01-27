// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PortableModule.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Autofac;
using Camera.Portable.Ioc;
using Camera.Portable.ViewModels;

namespace Camera.Portable.Modules
{
   /// <summary>
   ///    Portable module.
   /// </summary>
   public class PortableModule : IModule
   {
      #region Public Methods

      /// <summary>
      ///    Register the specified builder.
      /// </summary>
      /// <param name="builder">builder.</param>
      public void Register(ContainerBuilder builder)
      {
         builder.RegisterType<MainPageViewModel>().SingleInstance();
         builder.RegisterType<CameraPageViewModel>().SingleInstance();
      }

      #endregion
   }
}