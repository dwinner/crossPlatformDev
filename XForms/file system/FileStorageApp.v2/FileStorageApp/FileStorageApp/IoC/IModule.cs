﻿using Autofac;

namespace FileStorageApp.IoC
{
   /// <summary>
   ///    Module
   /// </summary>
   public interface IModule
   {
      /// <summary>
      ///    Register the specified builder
      /// </summary>
      /// <param name="builder">builder</param>
      void Register(ContainerBuilder builder);
   }
}