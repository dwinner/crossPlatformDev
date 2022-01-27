using System.Collections;
using Foundation;
using UIKit;

namespace Chat.iOS.Extras
{
   /// <summary>
   ///    Dictionary views.
   /// </summary>
   public class DictionaryViews : IEnumerable
   {
      /// <summary>
      ///    The ns dictionary.
      /// </summary>
      private readonly NSMutableDictionary nsDictionary;

      /// <summary>
      ///    Initializes a new instance of the <see cref="DictionaryViews" /> class.
      /// </summary>
      public DictionaryViews() => nsDictionary = new NSMutableDictionary();

      /// <summary>
      ///    Gets the enumerator.
      /// </summary>
      /// <returns>The enumerator.</returns>
      public IEnumerator GetEnumerator() => ((IEnumerable) nsDictionary).GetEnumerator();

      /// <summary>
      ///    Add the specified name and view.
      /// </summary>
      /// <param name="name">Name.</param>
      /// <param name="view">View.</param>
      public void Add(string name, UIView view)
      {
         nsDictionary.Add(new NSString(name), view);
      }

      /// <summary>
      ///    Ops the implicit.
      /// </summary>
      /// <returns>The implicit.</returns>
      /// <param name="us">Us.</param>
      public static implicit operator NSDictionary(DictionaryViews us) => us.ToNSDictionary();

      /// <summary>
      ///    Tos the NSD ictionary.
      /// </summary>
      /// <returns>The NSD ictionary.</returns>
      public NSDictionary ToNSDictionary() => nsDictionary;
   }
}