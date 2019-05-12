using FluentValidation;
using FluentValidation.Results;
using System;

namespace Deribit.S4KTNET.Core.Supporting
{
    // https://docs.deribit.com/v2/#public-auth

    public class HelloResponse : ResponseBase
    {
        public string version { get; set; }
    }
}