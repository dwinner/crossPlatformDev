using System.Collections.Generic;

namespace SQLiteExample.Interfaces
{
   public interface IRepository
   {
      void SaveData<T>(T toStore);

      List<T> GetList<T>(int top = 0) where T : class, new();

      T GetData<T>() where T : class, new();

      T GetData<T, TU>(string para, TU val) where T : class, new();

      T GetData<T, TU, TV>(string para1, TU val1, string para2, TV val2) where T : class, new();

      void Delete<T>(T stored);

      int GetId<T>() where T : class, new();
   }
}