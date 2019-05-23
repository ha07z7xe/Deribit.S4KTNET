namespace Deribit.S4KTNET.Core
{
    public static class DeribitInstruments
    {
        public static class Future
        {
            // BTC-DMMMYY
            public const string BTC25MAR16 = "BTC-25MAR16";
            public const string BTC5AUG16 = "BTC-25MAR16";
            public const string BTC17MAY19 = "BTC-17MAY19";

            public const string ETH17MAY19 = "ETH-17MAY19";
            public const string ETH28JUN19 = "ETH-28JUN19";
        }

        public static class Perpetual
        {
            public const string BTCPERPETUAL = "BTC-PERPETUAL";
            public const string ETHPERPETUAL = "ETH-PERPETUAL";

        }

        public static class Option 
        {
            // BTC-DMMMYY-STRIKE-K 	
            public const string BTC25MAR16420C = "BTC-25MAR16-420-C";
        }
    }
}