using FluentValidation;
using FluentValidation.Results;
using System;

namespace Deribit.S4KTNET.Core.Supporting
{
    // https://docs.deribit.com/v2/#public-hello

    public class HelloResponse : ResponseBase
    {
        public string version { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<HelloResponseDto, HelloResponse>();
            }
        }
    }

    public class HelloResponseDto
    {
        public string version { get; set; }
    }
}