using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.AccountManagement
{
    // https://docs.deribit.com/v2/#private-get_subaccounts

    public class GetSubAccountsRequest : RequestBase
    {
        public bool? with_portfolio { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<GetSubAccountsRequest, GetSubAccountsRequestDto>();
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<GetSubAccountsRequest>
        {

        }
    }

    public class GetSubAccountsRequestDto
    {
        public bool? with_portfolio { get; set; }
    }
}