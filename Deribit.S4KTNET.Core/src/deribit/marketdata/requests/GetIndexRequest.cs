using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.MarketData
{
    // https://docs.deribit.com/v2/#public-get_index

    public class GetIndexRequest : RequestBase
    {
        public CurrencyCode currency { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetIndexRequest, GetIndexRequestDto>()
                    .ForMember(d => d.currency, o => o.MapFrom(s => s.currency.ToDeribitString()))
                    ;
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<GetIndexRequest>
        {
            public Validator()
            {
                //this.RuleFor(x => x.currency).NotEmpty();
            }
        }
    }

    internal class GetIndexRequestDto
    {
        public string currency { get; set; }
    }
}