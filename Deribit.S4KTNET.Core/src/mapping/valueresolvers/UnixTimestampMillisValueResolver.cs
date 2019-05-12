using System;
using AutoMapper;

namespace Deribit.S4KTNET.Core.Mapping
{
    class UnixTimestampMillisValueResolver : IValueConverter<long, DateTime>
    {
        public DateTime Convert(long sourceMember, ResolutionContext context)
        {
            return sourceMember.UnixTimeStampMillisToDateTimeUtc();
        }
    }
}
