using System;

namespace Deribit.S4KTNET.Core
{
    public static class DeribitSubscriptions
    {
        public const string announcements = "announcements";

        public static string book(string instrumentName, OrderbookGrouping group, 
            OrderbookDepth depth, Interval Interval)
        {
            return $"book.{group}.{group.ToDeribitString()}";
        }
    }
}
