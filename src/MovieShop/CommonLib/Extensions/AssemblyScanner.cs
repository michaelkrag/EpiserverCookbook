using System;

namespace CommonLib.Extensions
{
    public static class AssemblyScanner
    {
        public static Type[] ForAssamby<TAssembly>()
        {
            return typeof(TAssembly).GetAssembly();
        }
    }
}