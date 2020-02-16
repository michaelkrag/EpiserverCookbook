using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonLib.Cache.Infrastructor
{
    public class AsyncLock : IDisposable
    {
        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public async Task<AsyncLock> LockAsync(TimeSpan timeout)
        {
            if (!await _semaphoreSlim.WaitAsync(timeout))
            {
                throw new TimeoutException($"LockAsync timeout for thread {Thread.CurrentThread.ManagedThreadId}");
            }
            return this;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _semaphoreSlim.Release();
            }
        }
    }
}