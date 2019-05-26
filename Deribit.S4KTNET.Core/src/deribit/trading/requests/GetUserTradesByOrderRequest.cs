using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.Trading
{
    // https://docs.deribit.com/v2/#private-get_user_trades_by_order

    public class GetUserTradesByOrderRequest : RequestBase
    {
        public string order_id { get; set; }

        public string sorting { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetUserTradesByOrderRequest, GetUserTradesByOrderRequestDto>();
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<GetUserTradesByOrderRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.order_id).NotEmpty();
                this.RuleFor(x => x.sorting)
                    .Must(x => x == null || x == "default" || x == "asc" || x == "desc");
            }
        }
    }

    internal class GetUserTradesByOrderRequestDto
    {
        public string order_id { get; set; }

        public string sorting { get; set; }
    }
}