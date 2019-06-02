using Deribit.S4KTNET.Core.Mapping;
using System;

namespace Deribit.S4KTNET.Core
{
    public class InstrumentDto
    {
        public string base_currency { get; set; }

        public int contract_size { get; set; }

        public long creation_timestamp { get; set; }

        public long expiration_timestamp { get; set; }

        public string instrument_name { get; set; }

        public bool is_active { get; set; }

        public string kind { get; set; }

        public decimal min_trade_amount { get; set; }

        public string option_type { get; set; }

        public string quote_currency { get; set; }

        public string settlement_period { get; set; }

        public decimal? strike { get; set; }

        public decimal tick_size { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<InstrumentDto, Instrument>()
                    .ForMember(d => d.InstrumentName, o => o.MapFrom(s => s.instrument_name))
                    .ForMember(d => d.QuoteCurrency, o => o.MapFrom(s => s.quote_currency))
                    .ForMember(d => d.BaseCurrency, o => o.MapFrom(s => s.base_currency.ToCurrencyCode()))
                    .ForMember(d => d.Url, o => o.Ignore())
                    .ForMember(d => d.TickSize, o => o.MapFrom(s => s.tick_size))
                    .ForMember(d => d.ContractSize, o => o.MapFrom(s => s.contract_size))
                    .ForMember(d => d.TakerFee, o => o.Ignore())
                    .ForMember(d => d.MakerFee, o => o.Ignore())
                    .ForMember(d => d.CreationTimestamp, o => o.MapFrom(s => s.creation_timestamp.UnixTimeStampMillisToDateTimeUtc()))
                    .ForMember(d => d.ExpirationTimestamp, o => o.MapFrom(s => s.expiration_timestamp.UnixTimeStampMillisToDateTimeUtc()))
                    .ForMember(d => d.IsActive, o => o.MapFrom(s => s.is_active))
                    .ForMember(d => d.Kind, o => o.MapFrom(s => s.kind.ToInstrumentKind()))
                    .ForMember(d => d.MinTradeAmount, o => o.MapFrom(s => s.min_trade_amount))
                    .ForMember(d => d.OptionType, o => o.MapFrom(s => s.option_type))
                    .ForMember(d => d.SettlementPeriod, o => o.MapFrom(s => s.settlement_period))
                    .ForMember(d => d.Strike, o => o.MapFrom(s => s.strike))
                    ;
            }
        }
    }
}