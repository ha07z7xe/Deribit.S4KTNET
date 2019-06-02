using Deribit.S4KTNET.Core.Mapping;
using System;

namespace Deribit.S4KTNET.Core
{
    public class PortfolioDto
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
                this.CreateMap<PortfolioDto, Portfolio>()
                    .ForMember(d => d.currency, o => o.MapFrom(s => s.currency.ToCurrencyCode()))
                    ;
            }
        }
    }
}