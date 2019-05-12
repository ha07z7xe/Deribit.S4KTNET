using FluentValidation;
using FluentValidation.Results;
using System;

namespace Deribit.S4KTNET.Core.Supporting
{
    // https://docs.deribit.com/v2/#public-auth

    public class HelloRequest : RequestBase
    {
        public string client_name { get; set; }

        public string client_version { get; set; }

        public override void Validate()
        {
            base.Validate();
            new Validator().ValidateAndThrow(this);
        }

        private class Validator : AbstractValidator<HelloRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.client_name).NotEmpty();
                this.RuleFor(x => x.client_version).NotEmpty();
            }
        }
    }
}