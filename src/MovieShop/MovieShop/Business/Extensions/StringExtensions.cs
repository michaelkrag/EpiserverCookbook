using System;

namespace MovieShop.Business.Extensions
{
    public static class StringExtensions
    {
        public static string TruncateString(this string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            return str.Substring(0, Math.Min(str.Length, maxLength));
        }
    }
}