using FluentValidation;
using FluentValidation.Results;
using System;

namespace Deribit.S4KTNET.Core.Authentication
{
    // https://docs.deribit.com/v2/#private-logout

    public class LogoutRequest : RequestBase
    {
        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<LogoutRequest, LogoutRequestDto>();
            }
        }
    }

    internal class LogoutRequestDto
    {

    }
}