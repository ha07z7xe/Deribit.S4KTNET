using Deribit.S4KTNET.Core.Mapping;
using System;

namespace Deribit.S4KTNET.Core.MarketData
{
    internal class CurrencyDto
    {
        public string coin_type { get; set; }

        public string currency { get; set; }

        public string currency_long { get; set; }

        public bool disabled_deposit_address_creation { get; set; }

        public int fee_precision { get; set; }

        public int min_confirmations { get; set; }

        public decimal min_withdrawal_fee { get; set; }

        public decimal withdrawal_fee { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<CurrencyDto, Currency>()
                    .ForMember(d => d.currency, o => o.MapFrom(s => s.currency.ToCurrencyCode()))
                    ;
            }
        }
    }
}