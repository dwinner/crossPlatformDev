using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageApp.Threading
{
   /// <summary>
   ///    Async semaphore
   /// </summary>
   public class AsyncSemaphore
   {
      private static readonly Task _Completed = Task.FromResult(true); // completed task
      private readonly Queue<TaskCompletionSource<bool>> _waiters = new Queue<TaskCompletionSource<bool>>(); // waiters
      private int _currentCount;

      /// <summary>
      ///    Initializes a new instance of the <see cref="AsyncSemaphore" />
      /// </summary>
      /// <param name="initialCount">Initial count</param>
      public AsyncSemaphore(int initialCount)
      {
         if (initialCount < 0)
         {
            throw new ArgumentOutOfRangeException(nameof(initialCount));
         }

         _currentCount = initialCount;
      }

      /// <summary>
      ///    Waits the async
      /// </summary>
      /// <returns>The pupet task</returns>
      public Task WaitAsync()
      {
         lock (_waiters)
         {
            if (_currentCount > 0)
            {
               --_currentCount;
               return _Completed;
            }

            var waiter = new TaskCompletionSource<bool>();
            _waiters.Enqueue(waiter);
            return waiter.Task;
         }
      }

      /// <summary>
      ///    Release this instance
      /// </summary>
      public void Release()
      {
         TaskCompletionSource<bool> toRelease = null;

         lock (_waiters)
         {
            if (_waiters.Count > 0)
            {
               toRelease = _waiters.Dequeue();
            }
            else
            {
               ++_currentCount;
            }
         }

         toRelease?.SetResult(true);
      }
   }
}