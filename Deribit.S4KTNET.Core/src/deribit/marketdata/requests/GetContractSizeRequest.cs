using FluentValidation;

namespace Deribit.S4KTNET.Core.MarketData
{
    // https://docs.deribit.com/v2/#public-get_contract_size

    public class GetContractSizeRequest : RequestBase
    {
        public string instrument_name { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetContractSizeRequest, GetContractSizeRequestDto>();
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<GetContractSizeRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.instrument_name).NotEmpty();
            }
        }
    }

    public class GetContractSizeRequestDto
    {
        public string instrument_name { get; set; }
    }
}