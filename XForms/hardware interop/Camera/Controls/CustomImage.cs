// --------------------------------------------------------------------------------------------------
//  <copyright file="CustomImage.cs" company="Flush Arcade Pty Ltd.">
//    Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using Xamarin.Forms;

namespace Camera.Controls
{
   /// <summary>
   ///    Custom image.
   /// </summary>
   public class CustomImage : View
   {
      /// <summary>
      ///    The tint color string property.
      /// </summary>
      /// <summary>
      ///    The tint on property.
      /// </summary>
      public static readonly BindableProperty TintColorStringProperty = BindableProperty.Create(
         (CustomImage o) => o.TintColorString, string.Empty,
         propertyChanged: (bindable, oldvalue, newValue) =>
         {
            var eh = ((CustomImage) bindable).CustomPropertyChanged;

            if (eh != null)
            {
               eh(bindable, TintColorStringProperty.PropertyName);
            }
         });

      /// <summary>
      ///    The tint on property.
      /// </summary>
      public static readonly BindableProperty TintOnProperty = BindableProperty.Create((CustomImage o) => o.TintOn,
         default,
         propertyChanged: (bindable, oldvalue, newValue) =>
         {
            var eh = ((CustomImage) bindable).CustomPropertyChanged;

            if (eh != null)
            {
               eh(bindable, TintOnProperty.PropertyName);
            }
         });

      /// <summary>
      ///    The path property.
      /// </summary>
      public static readonly BindableProperty PathProperty = BindableProperty.Create((CustomImage o) => o.Path, default,
         propertyChanged: (bindable, oldvalue, newValue) =>
         {
            var eh = ((CustomImage) bindable).CustomPropertyChanged;

            if (eh != null)
            {
               eh(bindable, PathProperty.PropertyName);
            }
         });

      /// <summary>
      ///    The aspect property.
      /// </summary>
      public static readonly BindableProperty AspectProperty = BindableProperty.Create((CustomImage o) => o.Aspect,
         default,
         propertyChanged: (bindable, oldvalue, newValue) =>
         {
            var eh = ((CustomImage) bindable).CustomPropertyChanged;

            if (eh != null)
            {
               eh(bindable, AspectProperty.PropertyName);
            }
         });

      /// <summary>
      ///    Gets or sets the tint color string.
      /// </summary>
      /// <value>The tint color string.</value>
      public string TintColorString
      {
         get => (string) GetValue(TintColorStringProperty);
         set => SetValue(TintColorStringProperty, value);
      }

      /// <summary>
      ///    Gets or sets a value indicating whether this <see cref="Camera.XamForms.Controls.CustomImage" /> tint on.
      /// </summary>
      /// <value><c>true</c> if tint on; otherwise, <c>false</c>.</value>
      public bool TintOn
      {
         get => (bool) GetValue(TintOnProperty);
         set => SetValue(TintOnProperty, value);
      }

      /// <summary>
      ///    Gets or sets the path.
      /// </summary>
      /// <value>The path.</value>
      public string Path
      {
         get => (string) GetValue(PathProperty);
         set => SetValue(PathProperty, value);
      }

      /// <summary>
      ///    Gets or sets the aspect.
      /// </summary>
      /// <value>The aspect.</value>
      public Aspect Aspect
      {
         get => (Aspect) GetValue(AspectProperty);
         set => SetValue(AspectProperty, value);
      }

      /// <summary>
      ///    Occurs when custom property changed.
      /// </summary>
      public event EventHandler<string> CustomPropertyChanged;

      /// <param name="propertyName">The name of the property that changed.</param>
      /// <summary>
      ///    Call this method from a child class to notify that a change happened on a property.
      /// </summary>
      protected override void OnPropertyChanged(string propertyName)
      {
         base.OnPropertyChanged(propertyName);

         if (propertyName == TintColorStringProperty.PropertyName ||
             propertyName == TintOnProperty.PropertyName ||
             propertyName == AspectProperty.PropertyName)
         {
            if (CustomPropertyChanged != null)
            {
               CustomPropertyChanged(this, propertyName);
            }
         }
      }
   }
}