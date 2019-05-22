using System;
using System.Linq;
using System.Text;

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

        public static BuySell ToBuySell(this string buysell)
        {
            switch (buysell)
            {
                case "buy":
                case "Buy":
                    return BuySell.Buy;
                case "sell":
                case "Sell":
                    return BuySell.Sell;
                default:
                    throw new Exception();
            }
        }

        public static OrderType ToOrderType(this string ordertype)
        {
            switch (ordertype)
            {
                case "limit":
                    return OrderType.limit;
                case "market":
                    return OrderType.market;
                case "stop_limit":
                    return OrderType.stop_limit;
                case "stop_market":
                    return OrderType.stop_market;
                default:
                    throw new Exception();
            }
        }

        public static OrderState ToOrderState(this string orderstate)
        {
            switch (orderstate)
            {
                case "cancelled":
                    return OrderState.cancelled;
                case "open":
                    return OrderState.open;
                case "rejected":
                    return OrderState.rejected;
                case "filled":
                    return OrderState.filled;
                case "untriggered":
                    return OrderState.untriggered;
                case "closed":
                    return OrderState.closed;
                default:
                    throw new Exception();
            }
        }

        public static string ToDeribitString(this OrderState orderstate)
        {
            switch (orderstate)
            {
                case OrderState.untriggered:
                    return "untriggered";
                case OrderState.cancelled:
                    return "cancelled";
                case OrderState.filled:
                    return "filled";
                case OrderState.open:
                    return "open";
                case OrderState.rejected:
                    return "rejected";
                default:
                    throw new Exception();
            }
        }

        public static string ToDeribitString(this OrderType ordertype)
        {
            switch (ordertype)
            {
                case OrderType.limit:
                    return "limit";
                case OrderType.market:
                    return "market";
                case OrderType.stop_limit:
                    return "stop_limit";
                case OrderType.stop_market:
                    return "stop_market";
                default:
                    throw new Exception();
            }
        }

        public static OrderTimeInForce ToOrderTimeInForce(this string timeinforce)
        {
            switch (timeinforce)
            {
                case "good_til_cancelled":
                    return OrderTimeInForce.good_til_cancelled;
                case "fill_or_kill":
                    return OrderTimeInForce.fill_or_kill;
                case "immediate_or_cancel":
                    return OrderTimeInForce.immediate_or_cancel;
                default:
                    throw new Exception();
            }
        }

        public static string ToDeribitString(this OrderTimeInForce tif)
        {
            switch (tif)
            {
                case OrderTimeInForce.fill_or_kill:
                    return "fill_or_kill";
                case OrderTimeInForce.good_til_cancelled:
                    return "good_til_cancelled";
                case OrderTimeInForce.immediate_or_cancel:
                    return "immediate_or_cancel";
                default:
                    throw new Exception();
            }
        }

        public static OrderTriggerType ToOrderTriggerType(this string triggertype)
        {
            switch (triggertype)
            {
                case "":
                case null:
                case "none":
                    return OrderTriggerType.none;
                case "index_price":
                    return OrderTriggerType.index_price;
                case "mark_price":
                    return OrderTriggerType.mark_price;
                case "last_price":
                    return OrderTriggerType.last_price;
                default:
                    throw new Exception();
            }
        }

        public static string ToDeribitString(this OrderTriggerType triggertype)
        {
            switch (triggertype)
            {
                case OrderTriggerType.none:
                    return null;
                case OrderTriggerType.index_price:
                    return "index_price";
                case OrderTriggerType.mark_price:
                    return "mark_price";
                case OrderTriggerType.last_price:
                    return "last_price";
                default:
                    throw new Exception();
            }
        }

        public static string ToDeribitString(this BuySell buysell)
        {
            switch (buysell)
            {
                case BuySell.Buy:
                    return "buy";
                case BuySell.Sell:
                    return "sell";
                default:
                    throw new Exception();
            }
        }

        public static string ToDeribitString(this InstrumentKind instrumentKind)
        {
            switch (instrumentKind)
            {
                case InstrumentKind.any:
                    return "any";
                case InstrumentKind.future:
                    return "future";
                case InstrumentKind.option:
                    return "option";
                default:
                    throw new Exception();
            }
        }

        public static string ToDeribitString(this Currency currency)
        {
            switch (currency)
            {
                case Currency.any:
                    return "any";
                case Currency.BTC:
                    return "BTC";
                case Currency.ETH:
                    return "ETH";
                default:
                    throw new Exception();
            }
        }

        public static DateTime UnixTimeStampMillisToDateTimeUtc(this long unixTimeStampMillis)
        {
            return unixepoch.AddMilliseconds(unixTimeStampMillis);
        }

        public static long UnixTimeStampDateTimeUtcToMillis(this DateTime dt)
        {
            return Convert.ToInt64((dt - unixepoch).TotalMilliseconds);
        }

        public static string ByteArrayToHexString(this byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static byte[] HexStringToByteArray(this string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}