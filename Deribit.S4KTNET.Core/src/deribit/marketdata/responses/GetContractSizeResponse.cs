using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;
using System;

namespace Deribit.S4KTNET.Core.MarketData
{
    // https://docs.deribit.com/v2/#public-get_contract_size

    public class GetContractSizeResponse : ResponseBase
    {
        public int contract_size { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetContractSizeResponseDto, GetContractSizeResponse>();
            }
        }

        public class Validator : FluentValidation.AbstractValidator<GetContractSizeResponse>
        {
            public Validator()
            {
                this.RuleFor(x => x.contract_size).NotEmpty();
            }
        }
    }

    public class GetContractSizeResponseDto
    {
        public int contract_size { get; set; }
    }
}