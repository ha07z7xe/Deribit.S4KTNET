using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.AccountManagement
{
    // https://docs.deribit.com/v2/#private-get_positions

    public class GetPositionsRequest : RequestBase
    {
        public Currency currency { get; set; }

        public InstrumentKind? kind { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetPositionsRequest, GetPositionsRequestDto>()
                    .ForMember(d => d.currency, o => o.MapFrom(s => s.currency.ToDeribitString()))
                    .ForMember(d => d.kind, o =>
                    {
                        o.PreCondition(s => s.kind.HasValue);
                        o.MapFrom(s => s.kind.Value.ToDeribitString());
                    });
            }
        }

        internal class Validator : AbstractValidator<GetPositionsRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.currency).NotEmpty().NotEqual(Currency.any);
            }
        }
    }

    public class GetPositionsRequestDto
    {
        public string currency { get; set; }

        public string kind { get; set; }
    }
}