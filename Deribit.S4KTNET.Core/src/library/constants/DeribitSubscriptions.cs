using System;

namespace Deribit.S4KTNET.Core
{
    public static class DeribitSubscriptions
    {
        //-------------------------------------------------------------------
        // documentation
        //-------------------------------------------------------------------
        // https://docs.deribit.com/v2/#subscriptions
        //-------------------------------------------------------------------

        // https://docs.deribit.com/v2/#announcements
        public const string announcements = "announcements";

        // https://docs.deribit.com/v2/#book-instrument_name-group-depth-interval
        public static string book(string instrument, OrderbookGrouping group, 
            OrderbookDepth depth, Interval interval)
        {
            return $"book.{group}.{instrument}.{group.ToDeribitString()}" +
                $".{depth.ToDeribitString()}.{interval.ToDeribitString()}";
        }

        // https://docs.deribit.com/v2/#book-instrument_name-interval
        public static string book(string instrument, Interval interval)
        {
            return $"book.{instrument}.{interval.ToDeribitString()}";
        }

        // https://docs.deribit.com/v2/#deribit_price_index-index_name
        public static string deribit_price_index(string index)
        {
            return $"deribit_price_index.{index}";
        }

        // https://docs.deribit.com/v2/#deribit_price_ranking-index_name
        public static string deribit_price_ranking(string index)
        {
            return $"deribit_price_ranking.{index}";
        }

        // https://docs.deribit.com/v2/#estimated_expiration_price-index_name
        public static string estimated_expiration_price(string index)
        {
            return $"estimated_expiration_price.{index}";
        }

        public static class markprice
        {
            // https://docs.deribit.com/v2/#markprice-options-index_name
            public static string options(string index)
            {
                return $"markprice.options.{index}";
            }
        }

        // https://docs.deribit.com/v2/#perpetual-instrument_name-interval
        public static string perpetual(string instrument, Interval interval)
        {
            return $"perpetual.{instrument}.{interval.ToDeribitString()}";
        }

        // https://docs.deribit.com/v2/#quote-instrument_name
        public static string quote(string instrument)
        {
            return $"quote.{instrument}";
        }

        // https://docs.deribit.com/v2/#ticker-instrument_name-interval
        public static string ticker(string instrument, Interval interval)
        {
            return $"ticker.{instrument}.{interval.ToDeribitString()}";
        }

        // https://docs.deribit.com/v2/#trades-instrument_name-interval
        public static string trades(string instrument, Interval interval)
        {
            return $"trades.{instrument}.{interval.ToDeribitString()}";
        }

        public static class user
        {
            // https://docs.deribit.com/v2/#user-orders-instrument_name-interval
            public static string orders(string instrument, Interval interval)
            {
                return $"user.orders.{instrument}.{interval.ToDeribitString()}";
            }
            
            // https://docs.deribit.com/v2/#user-orders-kind-currency-interval
            public static string orders(string instrument, InstrumentKind instrumentKind, 
                Currency currency, Interval interval)
            {
                return $"user.orders.{instrument}.{instrumentKind}.{currency}.{interval.ToDeribitString()}";
            }

            // https://docs.deribit.com/v2/#user-portfolio-currency
            public static string portfolio(Currency currency)
            {
                return $"user.portfolio.{currency}";
            }

            // https://docs.deribit.com/v2/#user-trades-instrument_name-interval
            public static string trades(string instrument, Interval interval)
            {
                return $"user.trades.{instrument}.{interval.ToDeribitString()}";
            }

            // https://docs.deribit.com/v2/#user-trades-kind-currency-interval
            public static string trades(string instrument, Currency currency, Interval interval)
            {
                return $"user.trades.{instrument}.{currency}.{interval.ToDeribitString()}";
            }
        }
    }
}
