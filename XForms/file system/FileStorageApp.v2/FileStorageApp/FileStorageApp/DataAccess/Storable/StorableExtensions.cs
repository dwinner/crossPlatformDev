using System.Linq;
using System.Reflection;

namespace FileStorageApp.DataAccess.Storable
{
   /// <summary>
   ///    Storable extensions
   /// </summary>
   public static class StorableExtensions
   {
      /// <summary>
      ///    Creates the insert or replace query
      /// </summary>
      /// <param name="storable">Storable item</param>
      /// <returns></returns>
      public static string CreateInsertOrReplaceQuery(this IStorable storable)
      {
         var properties = storable.GetType().GetRuntimeProperties().ToArray();
         var tableName = storable.GetType().Name;
         var propertyNames = string.Empty;
         var propertyValues = string.Empty;

         for (var i = 0; i < properties.Length; i++)
         {
            var propertyName = properties[i];
            propertyNames += i == properties.Length - 1
               ? propertyName.Name
               : $"{propertyName.Name}, ";
            var propertyValue = propertyName.GetValue(storable);
            var valueString = propertyValue == null
               ? "null"
               : propertyValue is bool boolVal
                  ? $"'{(boolVal ? 1 : 0)}'"
                  : $"'{propertyValue}'";

            // if data is serialized
            if (propertyName.Name.Equals("Data") && !valueString.Equals("null"))
            {
               valueString = valueString.Replace("\"", "\\\"");
            }

            propertyValues += valueString + (i == properties.Length - 1 ? string.Empty : ", ");
         }

         return $"INSERT OR REPLACE INTO {tableName}({propertyNames}) VALUES ({propertyValues});";
      }
   }
}