using System.Collections.Generic;
using System.Threading.Tasks;
using MvvxSandboxApp.Core.Models;

// TODO: use IAsyncEnumerable where possible for large data sets

namespace MvvxSandboxApp.Core.Services.Interfaces
{
   public interface IUsaStateStorage
   {
      Task<IEnumerable<UsaState>> GetPagedUsaStatesAsync(
         int pageOffset = 0, int pageItemCount = 50, bool ascByHood = true);

      IAsyncEnumerable<UsaState> GetAllStatesAsync(bool ascByHood = true);

      Task<bool> RemoveStateAsync(UsaState stateToRemove);

      Task<bool> AddNewStateAsync(UsaState newState);

      Task<bool> UpdateStateAsync(UsaState stateToUpdate);

      Task<UsaState> LoadRelatedDetailAsync(int usaStateId);
   }
}