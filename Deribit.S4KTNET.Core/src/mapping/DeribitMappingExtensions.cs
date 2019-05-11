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
    }
}