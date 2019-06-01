using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.AccountManagement
{
    // https://docs.deribit.com/v2/#private-get_account_summary

    public class GetAccountSummaryRequest : RequestBase
    {
        public CurrencyCode currency { get; set; }

        public bool? extended { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetAccountSummaryRequest, GetAccountSummaryRequestDto>()
                    .ForMember(d => d.currency, o => o.MapFrom(s => s.currency.ToDeribitString()));
            }
        }

        internal class Validator : AbstractValidator<GetAccountSummaryRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.currency).NotEmpty().NotEqual(CurrencyCode.any);
            }
        }
    }

    public class GetAccountSummaryRequestDto
    {
        public string currency { get; set; }

        public bool? extended { get; set; }

        internal class Validator : FluentValidation.AbstractValidator<GetAccountSummaryRequestDto>
        {
            public Validator()
            {
                this.RuleFor(x => x.currency).NotEmpty().Must(x => x == "BTC" || x == "ETH");
            }
        }
    }
}