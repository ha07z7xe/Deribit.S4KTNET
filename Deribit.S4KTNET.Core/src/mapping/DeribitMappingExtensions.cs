using System;

namespace Deribit.S4KTNET.Core.Mapping
{
    internal static class DeribitMappingExtensions
    {
        private static readonly DateTime unixepoch 
            = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

        public static string ToDeribitString(this OrderbookGrouping group)
        {
            switch (group)
            {
                case OrderbookGrouping._none:
                    return "none";
                case OrderbookGrouping._1:
                    return "1";
                case OrderbookGrouping._2:
                    return "2";
                case OrderbookGrouping._5:
                    return "5";
                case OrderbookGrouping._10:
                    return "10";
                case OrderbookGrouping._25:
                    return "25";
                case OrderbookGrouping._100:
                    return "100";
                case OrderbookGrouping._250:
                    return "250";
                default:
                    throw new Exception();
            }
        }

        public static OrderbookGrouping ToOrderbookGrouping(this string group)
        {
            switch (group)
            {
                case "1":
                    return OrderbookGrouping._1;
                case "2":
                    return OrderbookGrouping._2;
                case "5":
                    return OrderbookGrouping._5;
                case "10":
                    return OrderbookGrouping._10;
                case "25":
                    return OrderbookGrouping._25;
                case "100":
                    return OrderbookGrouping._100;
                case "250":
                    return OrderbookGrouping._250;
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

        public static OrderbookDepth ToOrderbookDepth(this string depth)
        {
            switch(depth)
            {
                case "1":
                    return OrderbookDepth._1;
                case "10":
                    return OrderbookDepth._10;
                case "20":
                    return OrderbookDepth._20;
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

        public static Interval ToInterval(this string interval)
        {
            switch (interval)
            {
                case "raw":
                    return Interval.raw;
                case "100ms":
                    return Interval._100ms;
                default:
                    throw new Exception();
            }
        }

        public static DateTime UnixTimeStampMillisToDateTimeUtc(this long unixTimeStampMillis)
        {
            return unixepoch.AddMilliseconds(unixTimeStampMillis);
        }
    }
}