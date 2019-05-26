using FluentValidation;
using FluentValidation.Results;
using System;

namespace Deribit.S4KTNET.Core.Supporting
{
    // https://docs.deribit.com/v2/#public-test

    public class TestRequest : RequestBase
    {
        public string expected_result { get; set; }

        internal class Validator : AbstractValidator<TestRequest>
        {
            public Validator()
            {
                this.RuleFor(x => x.expected_result).Equal("exception")
                    .When(x => x.expected_result != null);
            }
        }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<TestRequest, TestRequestDto>();
            }
        }

    }

    internal class TestRequestDto
    {
        public string expected_result { get; set; }
    }
}