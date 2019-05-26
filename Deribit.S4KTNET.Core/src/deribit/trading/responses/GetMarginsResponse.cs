using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Deribit.S4KTNET.Core.Trading
{
    public class GetMarginsResponse : ResponseBase
    {
        public decimal buy { get; set; }

        public decimal max_price { get; set; }

        public decimal min_price { get; set; }

        public decimal sell { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetMarginsResponseDto, GetMarginsResponse>();
            }
        }

        public class Validator : FluentValidation.AbstractValidator<GetMarginsResponse>
        {
            public Validator()
            {
                this.RuleFor(x => x.buy).NotEmpty();
                this.RuleFor(x => x.max_price).GreaterThan(x => x.min_price);
            }
        }
    }

    internal class GetMarginsResponseDto
    {
        public decimal buy { get; set; }

        public decimal max_price { get; set; }

        public decimal min_price { get; set; }

        public decimal sell { get; set; }
    }
}