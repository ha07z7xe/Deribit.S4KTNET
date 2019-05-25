using System;
using System.Threading;
using Deribit.S4KTNET.Core.Mapping;
using FluentValidation;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    // https://docs.deribit.com/v2/#user-portfolio-currency

    public class UserPortfolioNotification : SubscriptionNotification<UserPortfolioData>
    {
        internal class Profile : AutoMapper.Profile
        {
            private int sequencenumber = 1 << 10;
            public Profile()
            {
                this.CreateMap<UserPortfolioNotificationDto, UserPortfolioNotification>()
                    .ForMember(d => d.channelprefix, o => o.MapFrom(s => DeribitChannelPrefix.userportfolio))
                    .ForMember(d => d.sequencenumber, o => o.MapFrom(s => Interlocked.Increment(ref sequencenumber)))
                    .IncludeBase(typeof(SubscriptionNotificationDto<UserPortfolioDataDto>), typeof(SubscriptionNotification<UserPortfolioData>));
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<UserPortfolioNotification>
        {
            public Validator()
            {
                this.RuleFor(x => x.channelprefix).Equal(DeribitChannelPrefix.userportfolio);
                this.RuleFor(x => x.data).SetValidator(new UserPortfolioData.Validator());
            }
        }
    }

    public class UserPortfolioNotificationDto : SubscriptionNotificationDto<UserPortfolioDataDto>
    {
        
    }

    public class UserPortfolioData
    {
        public decimal available_funds { get; set; }

        public decimal available_withdrawal_funds { get; set; }

        public decimal balance { get; set; }

        public string currency { get; set; }

        public decimal delta_total { get; set; }

        public decimal equity { get; set; }

        public decimal futures_pl { get; set; }

        public decimal futures_session_pl { get; set; }

        public decimal futures_session_upl { get; set; }

        public decimal initial_margin { get; set; }

        public decimal maintenance_margin { get; set; }

        public decimal margin_balance { get; set; }

        public decimal options_delta { get; set; }

        public decimal options_gamma { get; set; }

        public decimal options_pl { get; set; }

        public decimal options_session_pl { get; set; }

        public decimal options_session_upl { get; set; }

        public decimal options_theta { get; set; }

        public decimal options_vega { get; set; }

        public bool portfolio_margining_enabled { get; set; }

        public decimal projected_initial_margin { get; set; }

        public decimal projected_maintenance_margin { get; set; }

        public decimal session_funding { get; set; }

        public decimal session_rpl { get; set; }

        public decimal session_upl { get; set; }

        public decimal total_pl { get; set; }


        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<UserPortfolioDataDto, UserPortfolioData>();
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<UserPortfolioData>
        {
            public Validator()
            {
                this.RuleFor(x => x.currency).NotEmpty();
            }
        }
    }

    public class UserPortfolioDataDto
    {
        public decimal available_funds { get; set; }

        public decimal available_withdrawal_funds { get; set; }

        public decimal balance { get; set; }

        public string currency { get; set; }

        public decimal delta_total { get; set; }

        public decimal equity { get; set; }

        public decimal futures_pl { get; set; }

        public decimal futures_session_pl { get; set; }

        public decimal futures_session_upl { get; set; }

        public decimal initial_margin { get; set; }

        public decimal maintenance_margin { get; set; }

        public decimal margin_balance { get; set; }

        public decimal options_delta { get; set; }

        public decimal options_gamma { get; set; }

        public decimal options_pl { get; set; }

        public decimal options_session_pl { get; set; }

        public decimal options_session_upl { get; set; }

        public decimal options_theta { get; set; }

        public decimal options_vega { get; set; }

        public bool portfolio_margining_enabled { get; set; }

        public decimal projected_initial_margin { get; set; }

        public decimal projected_maintenance_margin { get; set; }

        public decimal session_funding { get; set; }

        public decimal session_rpl { get; set; }

        public decimal session_upl { get; set; }

        public decimal total_pl { get; set; }

    }
}
