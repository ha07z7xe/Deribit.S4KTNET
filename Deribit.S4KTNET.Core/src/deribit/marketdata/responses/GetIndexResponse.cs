using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;
using System;

namespace Deribit.S4KTNET.Core.MarketData
{
    // https://docs.deribit.com/v2/#public-get_index

    public class GetIndexResponse : ResponseBase
    {
        public decimal? BTC { get; set; }

        public decimal? ETH { get; set; }

        public decimal edp { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetIndexResponseDto, GetIndexResponse>();
            }
        }

        public class Validator : FluentValidation.AbstractValidator<GetIndexResponse>
        {
            public Validator()
            {
                this.RuleFor(x => x.BTC).GreaterThan(0).When(x => x.BTC.HasValue);
                this.RuleFor(x => x.ETH).GreaterThan(0).When(x => x.ETH.HasValue);
            }
        }
    }

    internal class GetIndexResponseDto
    {
        public decimal? BTC { get; set; }

        public decimal? ETH { get; set; }

        public decimal edp { get; set; }
    }
}