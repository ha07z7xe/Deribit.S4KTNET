//using Deribit.S4KTNET.Core.Mapping;
//using FluentValidation;

//namespace Deribit.S4KTNET.Core.Trading
//{
//    // https://docs.deribit.com/v2/#private-get_user_trades_by_currency

//    public class GetUserTradesByCurrencyRequest : RequestBase
//    {
//        public string instrument_name { get; set; }

//        public int? start_seq { get; set; }

//        public int? end_seq { get; set; }

//        public int? count { get; set; }

//        public bool? include_old { get; set; }

//        public string sorting { get; set; }


//        internal class Profile : AutoMapper.Profile
//        {
//            public Profile()
//            {
//                this.CreateMap<GetUserTradesByCurrencyRequest, GetUserTradesByCurrencyRequestDto>();
//            }
//        }

//        internal class Validator : FluentValidation.AbstractValidator<GetUserTradesByCurrencyRequest>
//        {
//            public Validator()
//            {
//                this.RuleFor(x => x.instrument_name).NotEmpty();
//                this.RuleFor(x => x.sorting)
//                    .Must(x => x == null || x == "default" || x == "asc" || x == "desc");
//            }
//        }
//    }

//    public class GetUserTradesByCurrencyRequestDto
//    {
//        public string instrument_name { get; set; }

//        public int? start_seq { get; set; }

//        public int? end_seq { get; set; }

//        public int? count { get; set; }

//        public bool? include_old { get; set; }

//        public string sorting { get; set; }
//    }
//}