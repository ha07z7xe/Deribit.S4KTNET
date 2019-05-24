namespace Deribit.S4KTNET.Core
{
    public class Currency
    {
        public string coin_type { get; set; }

        public CurrencyCode currency { get; set; }

        public string currency_long { get; set; }

        public bool disabled_deposit_address_creation { get; set; }

        public int fee_precision { get; set; }

        public int min_confirmations { get; set; }

        public decimal min_withdrawal_fee { get; set; }

        public decimal withdrawal_fee { get; set; }
    }
}
