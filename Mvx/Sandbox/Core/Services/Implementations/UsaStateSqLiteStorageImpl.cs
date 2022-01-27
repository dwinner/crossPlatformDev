using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MvvxSandboxApp.Core.Models;
using MvvxSandboxApp.Core.Services.Interfaces;
using SQLitePCL;

// TOREFACTOR: try...catch is a boilerplate-code there and should be reduced by AOP

namespace MvvxSandboxApp.Core.Services.Implementations
{
   public sealed class UsaStateSqLiteStorageImpl : IUsaStateStorage
   {
      private const string DbFileName = "usa_states.db";
      private readonly string _dbPath;
      private readonly ILogger<UsaStateSqLiteStorageImpl> _logger;

      public UsaStateSqLiteStorageImpl(IDbPath dbPath, ILogger<UsaStateSqLiteStorageImpl> logger)
      {
         _logger = logger;
         _dbPath = dbPath.GetDbPath(DbFileName);

         using var dbContext = new UsaStateDbContext(_dbPath);
         if (!dbContext.UsaStates.Any())
         {
            var usaStates = PredefinedUsaStates.UsaStates;
            var usaStateDetails = PredefinedUsaStateDetails.UsaStateDetails;
            Debug.Assert(usaStates.Length == usaStateDetails.Length,
               "usaStates.Length == usaStateDetails.Length");

            for (var i = 0; i < usaStates.Length; i++)
            {
               var usaState = usaStates[i];
               var usaStateDetail = usaStateDetails[i];
               usaState.UsaStateDetail = usaStateDetail;
               usaStateDetail.UsaState = usaState;
               dbContext.AddRange(usaState, usaStateDetail);
            }

#if DEBUG
            /*for (var i = 0; i < 1000; i++)
            {
               var dummyState = new UsaState
               {
                  HoodYear = 1000 + i,
                  Abbr = "Abbr " + i,
                  Capital = "Capital " + i,
                  CapitalSinceYear = 1000 + i,
                  StateName = "State name " + i
               };
               var dummyDetail = new UsaStateDetail
               {
                  Area = 1000.0F,
                  CountryRank = i % 5,
                  FlagImageUri = null,
                  Metropolian = 1_000_000,
                  Municipal = 1_000,
                  Notes = "Some notes",
                  StateRank = (i + 1) % 5,
                  StateUri = null
               };
               dummyState.UsaStateDetail = dummyDetail;
               dummyDetail.UsaState = dummyState;
               dbContext.AddRange(dummyState, dummyDetail);
            }*/
#endif

            var insertedCnt = dbContext.SaveChanges();
            Debug.Assert(insertedCnt == usaStates.Length);
         }

         Batteries_V2.Init();
      }

      public async Task<IEnumerable<UsaState>> GetPagedUsaStatesAsync(
         int pageOffset, int pageItemCount, bool ascByHood = true)
      {
         try
         {
            await using var dbContext = new UsaStateDbContext(_dbPath);
            var orderedUsaStates = ascByHood
               ? dbContext.UsaStates.OrderBy(state => state.HoodYear)
               : dbContext.UsaStates.OrderByDescending(state => state.HoodYear);

            var usaStates = await orderedUsaStates
               .Skip(pageOffset)
               .Take(pageItemCount).ToListAsync()
               .ConfigureAwait(false);

            return usaStates;
         }
         catch (Exception dbEx)
         {
            _logger.LogError(dbEx, dbEx.Message);
            return Enumerable.Empty<UsaState>();
         }
      }

      public async IAsyncEnumerable<UsaState> GetAllStatesAsync(bool ascByHood = true)
      {
         IEnumerable<UsaState> states;
         try
         {
            await using var dbCtx = new UsaStateDbContext(_dbPath);
            var orderedUsaStates = ascByHood
               ? dbCtx.UsaStates.OrderBy(state => state.HoodYear)
               : dbCtx.UsaStates.OrderByDescending(state => state.HoodYear);

            states = await orderedUsaStates.ToListAsync().ConfigureAwait(false);
         }
         catch (Exception dbEx)
         {
            _logger.LogError(dbEx, dbEx.Message);
            states = Enumerable.Empty<UsaState>();
         }

         foreach (var state in states)
         {
            yield return state;
         }
      }

      public async Task<bool> RemoveStateAsync(UsaState stateToRemove)
      {
         try
         {
            await using var dbContext = new UsaStateDbContext(_dbPath);
            dbContext.UsaStates.Remove(stateToRemove);
            var success = await dbContext.SaveChangesAsync().ConfigureAwait(false);

            return success == 1;
         }
         catch (Exception dbEx)
         {
            _logger.LogError(dbEx, dbEx.Message);
            return false;
         }
      }

      public async Task<bool> AddNewStateAsync(UsaState newState)
      {
         try
         {
            await using var dbContext = new UsaStateDbContext(_dbPath);
            await dbContext.UsaStates.AddAsync(newState).ConfigureAwait(false);
            var affected = await dbContext.SaveChangesAsync().ConfigureAwait(false);

            return affected == 1;
         }
         catch (Exception dbEx)
         {
            _logger.LogError(dbEx, dbEx.Message);
            return false;
         }
      }

      public async Task<bool> UpdateStateAsync(UsaState stateToUpdate)
      {
         try
         {
            await using var dbContext = new UsaStateDbContext(_dbPath);
            var foundState = await dbContext.FindAsync<UsaState>(stateToUpdate.UsaStateId).ConfigureAwait(false);
            if (foundState != null)
            {
               foundState.StateName = stateToUpdate.StateName;
               foundState.Abbr = stateToUpdate.Abbr;
               foundState.HoodYear = stateToUpdate.HoodYear;
               foundState.Capital = stateToUpdate.Capital;
               foundState.CapitalSinceYear = stateToUpdate.CapitalSinceYear;
               foundState.UsaStateDetail.Area = stateToUpdate.UsaStateDetail.Area;
               foundState.UsaStateDetail.Notes = stateToUpdate.UsaStateDetail.Notes;
               foundState.UsaStateDetail.StateUri = stateToUpdate.UsaStateDetail.StateUri;
               foundState.UsaStateDetail.FlagImageUri = stateToUpdate.UsaStateDetail.FlagImageUri;
               foundState.UsaStateDetail.Municipal = stateToUpdate.UsaStateDetail.Municipal;
               foundState.UsaStateDetail.Metropolian = stateToUpdate.UsaStateDetail.Metropolian;
               foundState.UsaStateDetail.StateRank = stateToUpdate.UsaStateDetail.StateRank;
               foundState.UsaStateDetail.CountryRank = stateToUpdate.UsaStateDetail.CountryRank;

               dbContext.UsaStates.Update(foundState);
               var affected = await dbContext.SaveChangesAsync().ConfigureAwait(false);

               return affected == 1;
            }

            return false;
         }
         catch (Exception dbEx)
         {
            _logger.LogError(dbEx, dbEx.Message);
            return false;
         }
      }

      public async Task<UsaState> LoadRelatedDetailAsync(int usaStateId)
      {
         try
         {
            await using var dbContext = new UsaStateDbContext(_dbPath);
            var foundState = await dbContext.UsaStates.FindAsync(usaStateId).ConfigureAwait(false);
            if (foundState != null)
            {
               await dbContext.Entry(foundState)
                  .Reference(state => state.UsaStateDetail)
                  .LoadAsync()
                  .ConfigureAwait(false);

               return foundState;
            }

            _logger.LogDebug($"Usa state with {usaStateId} is not found");
            return null;
         }
         catch (Exception dbEx)
         {
            _logger.LogError(dbEx, dbEx.Message);
            return null;
         }
      }
   }
}