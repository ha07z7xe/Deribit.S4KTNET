using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deribit.S4KTNET.Core
{
    public class BookOrder
    {
        public BookOrderAction? action { get; set; }

        public decimal price { get; set; }

        public decimal amount { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<object[], BookOrder>()
                    .ConvertUsing((s, d, r) =>
                    {
                        if (s.Length == 2)
                        {
                            return new BookOrder()
                            {
                                price = r.Mapper.Map<decimal>(s[0]),
                                amount = r.Mapper.Map<decimal>(s[1]),
                            };
                        }
                        else if (s.Length == 3)
                        {
                            return new BookOrder()
                            {
                                action = (BookOrderAction) Enum.Parse(typeof(BookOrderAction), (string) s[0]),
                                price = r.Mapper.Map<decimal>(s[1]),
                                amount = r.Mapper.Map<decimal>(s[2]),
                            };
                        }
                        else
                        {
                            throw new Exception();
                        }
                    });
            }
        }

        internal class Validator : AbstractValidator<BookOrder>
        {
            public Validator()
            {
                this.RuleFor(x => x.price).NotEmpty();
                this.RuleFor(x => x.amount).NotEmpty();
            }
        }
    }

    public enum BookOrderAction
    {
        unknown,
        @new,
        change,
        delete,
    }
}
