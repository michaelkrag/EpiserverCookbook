using System;

namespace CommonLib.Cache
{
    public static class TimeOut
    {
        public static readonly TimeSpan AsyncLockTimeOut = new TimeSpan(0, 0, 10);
        public static readonly TimeSpan LockTimeOut = new TimeSpan(0, 0, 10);
        public static readonly TimeSpan LockCacheTimeOut = new TimeSpan(1, 0, 0);
    }
}