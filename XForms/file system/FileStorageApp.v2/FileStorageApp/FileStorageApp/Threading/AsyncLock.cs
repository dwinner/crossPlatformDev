using System;
using System.Threading;
using System.Threading.Tasks;

namespace FileStorageApp.Threading
{
   /// <summary>
   ///    Async lock
   /// </summary>
   public class AsyncLock
   {
      private readonly Task<Releaser> _releaser;
      private readonly AsyncSemaphore _semaphore; // The semaphore

      public AsyncLock()
      {
         _semaphore = new AsyncSemaphore(1);
         _releaser = Task.FromResult(new Releaser(this));
      }

      /// <summary>
      ///    Lock the async
      /// </summary>
      /// <returns>The async pupet task</returns>
      public Task<Releaser> LockAsync()
      {
         var wait = _semaphore.WaitAsync();
         return wait.IsCompleted
            ? _releaser
            : wait.ContinueWith((task, state) => new Releaser((AsyncLock) state), this, CancellationToken.None,
               TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
      }

      /// <summary>
      ///    Releaser
      /// </summary>
      public struct Releaser : IDisposable
      {
         private readonly AsyncLock _toRelease;

         internal Releaser(AsyncLock release) => _toRelease = release;

         public void Dispose() => _toRelease?._semaphore?.Release();
      }
   }
}