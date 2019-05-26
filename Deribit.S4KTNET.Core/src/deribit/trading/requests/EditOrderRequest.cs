using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.Trading
{
    // https://docs.deribit.com/v2/#private-edit

    public class EditOrderRequest : RequestBase
    {
        public string order_id { get; set; }

        public decimal amount { get; set; }

        public decimal price { get; set; }

        public bool post_only { get; set; }

        public decimal? stop_price { get; set; }

        public string advanced { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<EditOrderRequest, EditOrderRequestDto>();
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<EditOrderRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.order_id).NotEmpty();
                this.RuleFor(x => x.price).NotEmpty();
                this.RuleFor(x => x.amount).GreaterThan(0);
            }
        }
    }

    public class EditOrderRequestDto
    {
        public string order_id { get; set; }

        public decimal amount { get; set; }

        public decimal price { get; set; }

        public bool post_only { get; set; }

        public decimal? stop_price { get; set; }

        public string advanced { get; set; }
    }
}