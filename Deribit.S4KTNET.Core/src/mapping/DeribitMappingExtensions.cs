using System;

namespace Deribit.S4KTNET.Core
{
    internal static class DeribitMappingExtensions
    {
        public static string ToDeribitString(this OrderbookGrouping group)
        {
            switch (group)
            {
                case OrderbookGrouping._none:
                    return "none";
                case OrderbookGrouping._1:
                    return "1";
                default:
                    throw new Exception();
            }
        }

        public static string ToDeribitString(this OrderbookDepth depth)
        {
            switch (depth)
            {
                case OrderbookDepth._1:
                    return "1";
                case OrderbookDepth._10:
                    return "10";
                case OrderbookDepth._20:
                    return "20";
                default:
                    throw new Exception();
            }
        }

        public static string ToDeribitString(this Interval interval)
        {
            switch (interval)
            {
                case Interval.raw:
                    return "raw";
                case Interval._100ms:
                    return "100ms";
                default:
                    throw new Exception();
            }
        }
    }
}