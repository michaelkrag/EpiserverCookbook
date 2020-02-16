using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonLib.Cache.Infrastructor
{
    public class Lock : IDisposable
    {
        private object lockObj;

        public Lock(object lockObj, TimeSpan timeout)
        {
            this.lockObj = lockObj;
            if (!Monitor.TryEnter(this.lockObj, timeout))
            {
                throw new TimeoutException("Lock timeout for thread {Thread.CurrentThread.ManagedThreadId}");
            }
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
                Monitor.Exit(lockObj);
            }
        }
    }
}