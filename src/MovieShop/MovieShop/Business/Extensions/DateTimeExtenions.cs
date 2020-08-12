using System;

namespace MovieShop.Business.Extensions
{
    public static class DateTimeExtenions
    {
        public static string ToYear(this DateTime? dateTime)
        {
            //2029-10-20 19:19:48Z
            if (dateTime.HasValue)
            {
                return dateTime.Value.ToString("yyyy");
            }
            return string.Empty;
        }
    }
}